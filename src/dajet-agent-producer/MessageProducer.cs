using DaJet.Metadata;
using DaJet.Utilities;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DaJet.Agent.Producer
{
    public interface IMessageProducer : IDisposable
    {
        //void CreateQueue(string name);
        void Publish(List<DatabaseMessage> messageBatch);
    }
    public sealed class MessageProducer: IMessageProducer
    {
        private const string LOG_TOKEN = "P-MSG";
        private const string PUBLISHER_CONFIRMATION_ERROR_MESSAGE = "The sending of the message has not been confirmed. Check the availability of the message broker.";

        private IModel Channel { get; set; }        
        private IBasicProperties Properties { get; set; }
        private MessageProducerSettings Settings { get; set; }
        public MessageProducer(IOptions<MessageProducerSettings> options)
        {
            Settings = options.Value;
        }
        public void CreateQueue(string name)
        {
            InitializeChannel();

            Channel.ExchangeDeclare(name, ExchangeType.Direct, true, false, null);
            QueueDeclareOk queue = Channel.QueueDeclare(name, true, false, false, null);
            if (queue == null)
            {
                throw new InvalidOperationException($"Creating \"{name}\" queue failed.");
            }
            Channel.QueueBind(name, name, string.Empty, null);
        }
                
        private void InitializeConnection()
        {
            if (Connection == null)
            {
                Connection = CreateConnection();
            }
            else if (!Connection.IsOpen)
            {
                Connection.Dispose();
                Connection = CreateConnection();
            }
        }
        private void InitializeBasicProperties()
        {
            if (Channel == null) return;
            if (Channel.IsClosed) return;

            Properties = Channel.CreateBasicProperties();
            Properties.ContentType = "application/json";
            Properties.DeliveryMode = 2; // persistent
        }
        private void InitializeChannel()
        {
            InitializeConnection();

            if (Channel == null)
            {
                Channel = Connection.CreateModel();
                Channel.ConfirmSelect();
                InitializeBasicProperties();
            }
            else if (Channel.IsClosed)
            {
                Channel.Dispose();
                Channel = Connection.CreateModel();
                Channel.ConfirmSelect();
                InitializeBasicProperties();
            }
        }
        
        private string CreateExchangeName(string sender, string recipient)
        {
            return $"РИБ.{sender}.{recipient}";
        }
        private string[] GetRecipients(DatabaseMessage message)
        {
            return message.Recipients.Split(',', StringSplitOptions.RemoveEmptyEntries);
        }
        private JsonDataTransferMessage ProduceDataTransferMessage(DatabaseMessage data)
        {
            JsonDataTransferMessage message = new JsonDataTransferMessage()
            {
                Sender = data.Sender
            };
            message.Objects.Add(new JsonDataTransferObject()
            {
                Type = data.MessageType,
                Body = data.MessageBody,
                Operation = data.OperationType
            });
            return message;
        }
        
        private IConnection Connection { get; set; }
        private List<SendingChannel> Channels { get; set; } = new List<SendingChannel>();
        private IConnection CreateConnection()
        {
            IConnectionFactory factory = new ConnectionFactory()
            {
                HostName = Settings.MessageBrokerSettings.HostName,
                UserName = Settings.MessageBrokerSettings.UserName,
                Password = Settings.MessageBrokerSettings.Password,
                Port = Settings.MessageBrokerSettings.PortNumber
            };
            return factory.CreateConnection();
        }
        private IModel CreateChannel()
        {
            if (Channel == null)
            {
                Channel = Connection.CreateModel();
                return Channel;
            }
            else if (Channel.IsOpen)
            {
                return Channel;
            }
            Channel.Dispose();

            Channel = Connection.CreateModel();
            
            return Channel;
        }
        private SendingChannel CreateSendingChannel()
        {
            IModel channel = Connection.CreateModel();
            channel.ConfirmSelect();
            IBasicProperties properties = channel.CreateBasicProperties();
            properties.ContentType = "application/json";
            properties.DeliveryMode = 2; // persistent
            return new SendingChannel()
            {
                Channel = channel,
                Properties = properties
            };
        }
        private bool ExchangeExists(string exchange)
        {
            bool exists = true;
            IModel channel = CreateChannel();
            try
            {
                channel.ExchangeDeclarePassive(exchange);
            }
            catch (TimeoutException timeoutError)
            {
                exists = false;
                string errorMessage = "Check if exists \"" + exchange + "\". " + timeoutError.Message;
                Exception exception = new Exception(errorMessage);
                SendingExceptions.Enqueue(exception);
            }
            catch (Exception error)
            {
                exists = false;
                SendingExceptions.Enqueue(error);
            }
            return exists;
        }

        private void ConfigureConnection()
        {
            if (Connection != null && Connection.IsOpen) return;
            if (Connection != null) Connection.Dispose();

            Connection = CreateConnection();
            Connection.ConnectionBlocked += HandleConnectionBlocked;
            Connection.ConnectionUnblocked += HandleConnectionUnblocked;
        }
        private void HandleConnectionBlocked(object sender, ConnectionBlockedEventArgs args)
        {
            ConnectionIsBlocked = true;
            FileLogger.Log(LOG_TOKEN, "Connection blocked: " + args.Reason);

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
            FileLogger.Log(LOG_TOKEN, "Connection unblocked.");
        }
        private void ConfigureChannels()
        {
            int channelMax = Environment.ProcessorCount;
            if (Connection.ChannelMax > 0 && Connection.ChannelMax < channelMax)
            {
                channelMax = Connection.ChannelMax;
            }

            if (Channels.Count == 0)
            {
                for (int i = 0; i < channelMax; i++)
                {
                    Channels.Add(CreateSendingChannel());
                }
            }
            else
            {
                for (int i = 0; i < Channels.Count; i++)
                {
                    if (!Channels[i].IsHealthy)
                    {
                        Channels[i].Channel.Dispose();
                        Channels[i] = CreateSendingChannel();
                    }
                }
            }
        }

        public void Dispose()
        {
            DisposeChannel();
            DisposeChannels();
            DisposeConnection();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private void DisposeChannel()
        {
            if (Channel != null)
            {
                if (!Channel.IsClosed)
                {
                    Channel.Close();
                }
                Channel.Dispose();
                Channel = null;
            }
        }
        private void DisposeChannels()
        {
            if (Channels == null || Channels.Count == 0) return;

            for (int i = 0; i < Channels.Count; i++)
            {
                if (Channels[i].Channel != null)
                {
                    if (!Channels[i].Channel.IsClosed)
                    {
                        Channels[i].Channel.Close();
                        //Channels[i].Channel.Abort();
                    }
                    Channels[i].Channel.Dispose();
                    Channels[i].Channel = null;
                }
            }
            Channels.Clear();
        }
        private void DisposeConnection()
        {
            if (Connection != null)
            {
                if (Connection.IsOpen)
                {
                    Connection.Close();
                    //Connection.Abort();
                }
                Connection.ConnectionBlocked -= HandleConnectionBlocked;
                Connection.ConnectionUnblocked -= HandleConnectionUnblocked;
                //Connection.Dispose(); // can be locked on infinite wait !
                Connection = null;
            }
        }

        private bool ConnectionIsBlocked = false;
        private CancellationTokenSource SendingCancellation;
        private ConcurrentQueue<Exception> SendingExceptions;
        private Dictionary<string, Queue<DatabaseMessage>> ProducerQueues;
        private List<KeyValuePair<string, Queue<DatabaseMessage>>> ProducerQueuesList;
        
        public void Publish(List<DatabaseMessage> messageBatch)
        {
            if (messageBatch == null || messageBatch.Count == 0) return;

            if (ConnectionIsBlocked)
            {
                throw new OperationCanceledException("Connection was blocked: sending messages operation is canceled.");
            }

            bool throwException = false;

            SendingExceptions = new ConcurrentQueue<Exception>();
            
            ConfigureConnection();
            ConfigureChannels();

            int messagesSent = 0;
            int messagesToBeSent = 0;

            try
            {
                messagesToBeSent = PrepareProducerQueues(messageBatch);

                if (messagesToBeSent > 0)
                {
                    messagesSent = ProcessProducerQueuesInParallel();
                    
                    if (messagesSent != messagesToBeSent)
                    {
                        throwException = true;
                    }
                }
                else
                {
                    throwException = true;
                }
            }
            catch (Exception error)
            {
                throwException = true;
                SendingExceptions.Enqueue(error);
            }

            ProcessExceptions();

            if (throwException)
            {
                throw new OperationCanceledException("Sending messages operation was canceled.");
            }
        }
        private int PrepareProducerQueues(List<DatabaseMessage> messageBatch)
        {
            int messagesToSend = 0;

            ProducerQueues = new Dictionary<string, Queue<DatabaseMessage>>();

            string[] recipients = null;
            string exchangeName = string.Empty;
            foreach (DatabaseMessage message in messageBatch)
            {
                recipients = GetRecipients(message);
                if (recipients.Length == 0) continue;

                SerializeDatabaseMessage(message);

                foreach (string recipient in recipients)
                {
                    exchangeName = CreateExchangeName(message.Sender, recipient);

                    if (!ExchangeExists(exchangeName)) continue;

                    Queue<DatabaseMessage> queue;
                    if (!ProducerQueues.TryGetValue(exchangeName, out queue))
                    {
                        queue = new Queue<DatabaseMessage>();
                        ProducerQueues.Add(exchangeName, queue);
                    }
                    queue.Enqueue(message);
                    messagesToSend++;
                }
            }
            ProducerQueuesList = ProducerQueues.ToList();

            return messagesToSend;
        }
        private void SerializeDatabaseMessage(DatabaseMessage message)
        {
            JsonDataTransferMessage dtm = ProduceDataTransferMessage(message);
            string json = JsonSerializer.Serialize(dtm);
            message.MessageBytes = Encoding.UTF8.GetBytes(json);
        }
        private int ProcessProducerQueuesInParallel()
        {
            int messagesSent = 0;

            using (SendingCancellation = new CancellationTokenSource())
            {
                Task<int>[] tasks = new Task<int>[Channels.Count];

                for (int channelId = 0; channelId < Channels.Count; channelId++)
                {
                    tasks[channelId] = Task.Factory.StartNew(
                        ProcessProducerQueuesInBackground,
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
        private int ProcessProducerQueuesInBackground(object id)
        {
            int channelId = (int)id;

            SendingChannel sendingChannel = Channels[channelId];

            int messagesSent = 0;
            int nextQueue = channelId;
            List<string> messageQueues = new List<string>();
            while (nextQueue < ProducerQueuesList.Count)
            {
                var item = ProducerQueuesList[nextQueue];

                if (item.Value.Count == 0)
                {
                    nextQueue += Channels.Count;
                    continue;
                }

                while (item.Value.TryDequeue(out DatabaseMessage message))
                {
                    if (SendingCancellation.IsCancellationRequested) return 0;

                    try
                    {
                        sendingChannel.Channel.BasicPublish(item.Key, string.Empty, true, sendingChannel.Properties, message.MessageBytes);
                        messagesSent++;
                    }
                    catch (Exception error)
                    {
                        SendingExceptions.Enqueue(error);
                        SendingCancellation.Cancel();
                    }
                }

                nextQueue += Channels.Count;
                messageQueues.Add(item.Key);
            }
            if (messagesSent > 0)
            {
                try
                {
                    bool confirmed = sendingChannel.Channel.WaitForConfirms(TimeSpan.FromSeconds(Settings.MessageBrokerSettings.ConfirmationTimeout));
                    if (!confirmed)
                    {
                        SendingExceptions.Enqueue(new Exception(PUBLISHER_CONFIRMATION_ERROR_MESSAGE));
                        SendingCancellation.Cancel();
                    }
                }
                catch (OperationInterruptedException rabbitError)
                {
                    SendingExceptions.Enqueue(rabbitError);
                    if (string.IsNullOrWhiteSpace(rabbitError.Message) || !rabbitError.Message.Contains("NOT_FOUND"))
                    {
                        SendingCancellation.Cancel();
                    }
                }
                catch (Exception error)
                {
                    SendingExceptions.Enqueue(error);
                    SendingCancellation.Cancel();
                }

                string destinationQueues = string.Empty;
                foreach (string destination in messageQueues)
                {
                    destinationQueues += (string.IsNullOrEmpty(destinationQueues) ? destination : ", " + destination);
                }
                FileLogger.Log(LOG_TOKEN, $"{messagesSent} messages have been sent to destination queues: {destinationQueues}.");
            }

            return messagesSent;
        }
        private void ProcessExceptions()
        {
            if (SendingExceptions.Count > 0)
            {
                while (SendingExceptions.TryDequeue(out Exception error))
                {
                    FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error));
                }
            }
            SendingExceptions.Clear();
        }
    }
}