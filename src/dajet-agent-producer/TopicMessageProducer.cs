using DaJet.Metadata;
using DaJet.Utilities;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaJet.Agent.Producer
{
    public sealed class TopicMessageProducer : IMessageProducer
    {
        private const string PUBLISHER_EXCHANGE_IS_NOT_DEFINED_ERROR_MESSAGE = "Publisher exchange is not defined.";
        private const string PUBLISHER_EXCHANGE_DOES_NOT_EXIST_ERROR_MESSAGE = "Publisher exchange does not exist.";
        private const string CONNECTION_BLOCKED_ERROR_MESSAGE = "Connection is blocked: sending messages operation is canceled.";
        private const string PUBLISHER_CONFIRMATION_ERROR_MESSAGE = "The sending of the message has not been confirmed. Check the availability of the message broker.";
        private const string PUBLISHER_CONFIRMATION_TIMEOUT_MESSAGE = "The sending of the messages has been timed out. Check the availability of the message broker.";

        private CancellationTokenSource SendingCancellation;
        private MessageProducerSettings Settings { get; set; }
        public TopicMessageProducer(IOptions<MessageProducerSettings> options)
        {
            Settings = options.Value;
        }
        public void Dispose()
        {
            if (ProducerChannels != null)
            {
                foreach (ProducerChannel channel in ProducerChannels)
                {
                    if (channel.Channel != null)
                    {
                        if (!channel.Channel.IsClosed)
                        {
                            channel.Channel.Close();
                        }
                        channel.Channel.Dispose();
                        channel.Channel = null;
                    }
                    if (channel.Queues != null)
                    {
                        foreach (Queue<DatabaseMessage> queue in channel.Queues)
                        {
                            queue.Clear();
                        }
                        channel.Queues.Clear();
                    }
                }
            }
            
            if (Connection != null)
            {
                if (Connection.IsOpen)
                {
                    Connection.Close();
                }
                Connection.Dispose();
                Connection = null;
            }
        }

        public void Publish(List<DatabaseMessage> batch)
        {
            if (string.IsNullOrWhiteSpace(Settings.MessageBrokerSettings.TopicExchange))
            {
                throw new OperationCanceledException(PUBLISHER_EXCHANGE_IS_NOT_DEFINED_ERROR_MESSAGE);
            }

            if (batch == null || batch.Count == 0) return;

            if (ConnectionIsBlocked)
            {
                throw new OperationCanceledException(CONNECTION_BLOCKED_ERROR_MESSAGE);
            }

            bool throwException;
            ProducerExceptions = new ConcurrentQueue<Exception>();

            ConfigureConnection();

            if (!ExchangeExists(Settings.MessageBrokerSettings.TopicExchange))
            {
                throw new OperationCanceledException(PUBLISHER_EXCHANGE_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            ConfigureProducerChannels();
            ConfigureProducerQueues(batch);
            AssignProducerQueuesToChannels();

            int messagesSent = 0;
            int messagesToBeSent = batch.Count;
            try
            {
                messagesSent = PublishMessagesInParallel();

                throwException = (messagesSent != messagesToBeSent);
            }
            catch (Exception error)
            {
                throwException = true;
                ProducerExceptions.Enqueue(error);
            }

            LogExceptions();

            if (throwException)
            {
                throw new OperationCanceledException("Sending messages operation was canceled.");
            }
            else
            {
                FileLogger.Log(string.Format("{0} messages have been published successfully.", messagesSent));
            }
        }

        #region "RabbitMQ connection management"

        private IConnection Connection { get; set; }
        private bool ConnectionIsBlocked { get; set; }

        private IConnection CreateConnection()
        {
            IConnectionFactory factory = new ConnectionFactory()
            {
                HostName = Settings.MessageBrokerSettings.HostName,
                VirtualHost = Settings.MessageBrokerSettings.VirtualHost,
                UserName = Settings.MessageBrokerSettings.UserName,
                Password = Settings.MessageBrokerSettings.Password,
                Port = Settings.MessageBrokerSettings.PortNumber
            };
            return factory.CreateConnection();
        }
        private void ConfigureConnection()
        {
            if (Connection != null && Connection.IsOpen) return;
            if (Connection != null)
            {
                Connection.ConnectionBlocked -= HandleConnectionBlocked;
                Connection.ConnectionUnblocked -= HandleConnectionUnblocked;
                Connection.Dispose();
            }

            Connection = CreateConnection();
            Connection.ConnectionBlocked += HandleConnectionBlocked;
            Connection.ConnectionUnblocked += HandleConnectionUnblocked;
        }
        private void HandleConnectionBlocked(object sender, ConnectionBlockedEventArgs args)
        {
            ConnectionIsBlocked = true;
            FileLogger.Log("Connection blocked: " + args.Reason);

            if (SendingCancellation != null)
            {
                try
                {
                    SendingCancellation.Cancel();
                }
                catch
                {
                    // SendingCancellation can be already disposed ...
                }
            }
        }
        private void HandleConnectionUnblocked(object sender, EventArgs args)
        {
            ConnectionIsBlocked = false;
            FileLogger.Log("Connection unblocked.");
        }

        #endregion

        #region "RabbitMQ channels management"

        private List<ProducerChannel> ProducerChannels;
        
        private ProducerChannel CreateProducerChannel()
        {
            IModel channel = Connection.CreateModel();
            channel.ConfirmSelect(); // enable producer confirms
            return new ProducerChannel()
            {
                Channel = channel,
                Properties = CreateMessageProperties(channel)
            };
        }
        private IBasicProperties CreateMessageProperties(IModel channel)
        {
            IBasicProperties properties = channel.CreateBasicProperties();
            properties.ContentType = "application/json";
            properties.DeliveryMode = 2; // persistent
            properties.ContentEncoding = "UTF-8";
            return properties;
        }
        private void ConfigureProducerChannels()
        {
            int channelMax = Environment.ProcessorCount;
            if (Connection.ChannelMax > 0 && Connection.ChannelMax < channelMax)
            {
                channelMax = Connection.ChannelMax;
            }

            if (ProducerChannels == null)
            {
                ProducerChannels = new List<ProducerChannel>(channelMax);
            }

            if (ProducerChannels.Count == 0)
            {
                for (int i = 0; i < channelMax; i++)
                {
                    ProducerChannels.Add(CreateProducerChannel());
                }
            }
            else
            {
                for (int i = 0; i < ProducerChannels.Count; i++)
                {
                    ProducerChannels[i].Queues.Clear();
                    if (!ProducerChannels[i].IsHealthy)
                    {
                        ProducerChannels[i].Channel.Dispose();
                        ProducerChannels[i] = CreateProducerChannel();
                    }
                }
            }
        }

        #endregion

        #region "Assinging messages to channels"

        private Dictionary<string, Queue<DatabaseMessage>> ProducerQueues;

        private void ConfigureProducerQueues(List<DatabaseMessage> batch)
        {
            if (ProducerQueues != null)
            {
                ProducerQueues.Clear();
            }
            else
            {
                ProducerQueues = new Dictionary<string, Queue<DatabaseMessage>>();
            }

            foreach (DatabaseMessage message in batch)
            {
                Queue<DatabaseMessage> queue;
                if (!ProducerQueues.TryGetValue(message.MessageType, out queue))
                {
                    queue = new Queue<DatabaseMessage>();
                    ProducerQueues.Add(message.MessageType, queue);
                }
                queue.Enqueue(message);
            }
        }
        private void AssignProducerQueuesToChannels()
        {
            int nextChannel = 0;
            int maxChannels = ProducerChannels.Count;
            if (maxChannels == 0) return;

            foreach (Queue<DatabaseMessage> queue in ProducerQueues.Values)
            {
                if (nextChannel == maxChannels)
                {
                    nextChannel = 0;
                }

                ProducerChannels[nextChannel].Queues.Add(queue);

                nextChannel++;
            }

            ProducerQueues.Clear();
        }

        #endregion

        #region "RabbitMQ exchange management"

        private bool ExchangeExists(string exchange)
        {
            bool exists = true;
            using (IModel channel = Connection.CreateModel())
            {
                try
                {
                    channel.ExchangeDeclarePassive(exchange);
                }
                catch (TimeoutException timeoutError)
                {
                    exists = false;
                    string errorMessage = "Check if exists \"" + exchange + "\". " + timeoutError.Message;
                }
                catch (Exception error)
                {
                    exists = false;
                    string errorMessage = error.Message;
                }
            }
            return exists;
        }

        #endregion

        #region "Publishing messages"

        private ConcurrentQueue<Exception> ProducerExceptions;

        private int PublishMessagesInParallel()
        {
            int messagesSent = 0;

            using (SendingCancellation = new CancellationTokenSource())
            {
                Task<int>[] tasks = new Task<int>[ProducerChannels.Count];

                for (int channelId = 0; channelId < ProducerChannels.Count; channelId++)
                {
                    tasks[channelId] = Task.Factory.StartNew(
                        PublishMessagesInBackground,
                        channelId,
                        SendingCancellation.Token,
                        TaskCreationOptions.LongRunning,
                        TaskScheduler.Default);
                }

                Task.WaitAll(tasks, SendingCancellation.Token);

                foreach (Task<int> task in tasks)
                {
                    messagesSent += task.Result;
                }
            }

            return messagesSent;
        }
        private int PublishMessagesInBackground(object id)
        {
            int channelId = (int)id;

            ProducerChannel producerChannel = ProducerChannels[channelId];

            int messagesSent = 0;

            foreach (Queue<DatabaseMessage> queue in producerChannel.Queues)
            {
                while (queue.TryDequeue(out DatabaseMessage message))
                {
                    if (SendingCancellation.IsCancellationRequested) return 0;

                    try
                    {
                        PublishMessage(producerChannel, message);
                        messagesSent++;
                    }
                    catch (Exception error)
                    {
                        ProducerExceptions.Enqueue(error);
                        SendingCancellation.Cancel();
                    }
                }
            }

            if (messagesSent > 0)
            {
                WaitForConfirms(producerChannel);
            }

            return messagesSent;
        }

        private void ConfigureMessageProperties(DatabaseMessage message, IBasicProperties properties)
        {
            properties.AppId = message.Sender;
            properties.Type = message.MessageType;
            properties.MessageId = message.Uuid.ToString();
            
            if (properties.Headers == null)
            {
                properties.Headers = new Dictionary<string, object>();
            }
            else
            {
                properties.Headers.Clear();
            }

            SetOperationTypeHeader(message, properties);

            if (Settings.MessageBrokerSettings.ExchangeRole == 0)
            {
                SetAggregatorCopyHeader(message, properties);
            }
            else
            {
                SetDispatcherCopyHeader(message, properties);
            }
        }
        private void SetOperationTypeHeader(DatabaseMessage message, IBasicProperties properties)
        {
            if (string.IsNullOrWhiteSpace(message.OperationType)) return;

            if (properties.Headers == null)
            {
                properties.Headers = new Dictionary<string, object>();
            }

            if (!properties.Headers.TryAdd("OperationType", message.OperationType))
            {
                properties.Headers["OperationType"] = message.OperationType;
            }
        }
        private void SetAggregatorCopyHeader(DatabaseMessage message, IBasicProperties properties)
        {
            if (string.IsNullOrWhiteSpace(message.Sender)) return;

            string header = (Settings.MessageBrokerSettings.CopyType == 0 ? "CC" : "BCC");
            
            properties.Headers.Add(header, new string[] { message.Sender });
        }
        private void SetDispatcherCopyHeader(DatabaseMessage message, IBasicProperties properties)
        {
            if (string.IsNullOrWhiteSpace(message.Recipients)) return;

            string header = (Settings.MessageBrokerSettings.CopyType == 0 ? "CC" : "BCC");

            properties.Headers.Add(header, message.Recipients.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }

        private void PublishMessage(ProducerChannel channel, DatabaseMessage message)
        {
            string exchange = Settings.MessageBrokerSettings.TopicExchange;

            string routingKey = message.MessageType;

            byte[] messageBytes = Encoding.UTF8.GetBytes(message.MessageBody);

            ConfigureMessageProperties(message, channel.Properties);

            channel.Channel.BasicPublish(exchange, routingKey, channel.Properties, messageBytes);
        }
        private void WaitForConfirms(ProducerChannel channel)
        {
            try
            {
                int confirmationTimeout = Settings.MessageBrokerSettings.ConfirmationTimeout;
                bool confirmed = channel.Channel.WaitForConfirms(TimeSpan.FromSeconds(confirmationTimeout), out bool timedout);
                if (!confirmed)
                {
                    if (timedout)
                    {
                        ProducerExceptions.Enqueue(new OperationCanceledException(PUBLISHER_CONFIRMATION_TIMEOUT_MESSAGE));
                    }
                    else
                    {
                        ProducerExceptions.Enqueue(new OperationCanceledException(PUBLISHER_CONFIRMATION_ERROR_MESSAGE));
                    }
                    SendingCancellation.Cancel();
                }
            }
            catch (OperationInterruptedException rabbitError)
            {
                ProducerExceptions.Enqueue(rabbitError);
                if (string.IsNullOrWhiteSpace(rabbitError.Message) || !rabbitError.Message.Contains("NOT_FOUND"))
                {
                    SendingCancellation.Cancel();
                }
            }
            catch (Exception error)
            {
                ProducerExceptions.Enqueue(error);
                SendingCancellation.Cancel();
            }
        }

        #endregion

        private void LogExceptions()
        {
            if (ProducerExceptions.Count > 0)
            {
                while (ProducerExceptions.TryDequeue(out Exception error))
                {
                    FileLogger.Log(ExceptionHelper.GetErrorText(error));
                }
            }
            ProducerExceptions.Clear();
        }
    }
}