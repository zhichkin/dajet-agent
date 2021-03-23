﻿using DaJet.Metadata;
using DaJet.Utilities;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
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
        void CreateQueue(string name);
        void Send(DatabaseMessage message);
        void SendBatch(List<DatabaseMessage> messageBatch);
    }
    public sealed class MessageProducer: IMessageProducer
    {
        private const string LOG_TOKEN = "P-MSG";
        private const string QUEUE_NOT_FOUND_ERROR_TEMPLATE = "Queue \"{0}\" is not found.";
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
        public void Dispose()
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
        public void Send(DatabaseMessage message)
        {
            InitializeChannel();

            string[] recipients = GetRecipients(message);
            if (recipients.Length == 0) return;

            string exchangeName = string.Empty;
            foreach (string recipient in recipients)
            {
                exchangeName = CreateExchangeName(message.Sender, recipient);
                JsonDataTransferMessage dtm = ProduceDataTransferMessage(message);
                string json = JsonSerializer.Serialize(dtm);
                byte[] messageBytes = Encoding.UTF8.GetBytes(json);
                try
                {
                    Channel.BasicPublish(exchangeName, string.Empty, true, Properties, messageBytes);
                    bool confirmed = Channel.WaitForConfirms(TimeSpan.FromSeconds(Settings.MessageBrokerSettings.ConfirmationTimeout));
                    if (!confirmed)
                    {
                        throw new OperationCanceledException(PUBLISHER_CONFIRMATION_ERROR_MESSAGE);
                    }
                }
                catch (OperationInterruptedException rabbitError)
                {
                    if (!string.IsNullOrWhiteSpace(rabbitError.Message)
                        && rabbitError.Message.Contains("NOT_FOUND")
                        && rabbitError.Message.Contains(exchangeName))
                    {
                        // queue not found
                        FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(rabbitError));
                    }
                    else
                    {
                        throw;
                    }
                }
            }
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
        
        private CancellationTokenSource SendingCancellation;
        private ConcurrentQueue<Exception> SendingExceptions;
        private List<KeyValuePair<string, Queue<DatabaseMessage>>> ProducerQueuesList;
        private Dictionary<string, Queue<DatabaseMessage>> ProducerQueues = new Dictionary<string, Queue<DatabaseMessage>>();
        public void SendBatch(List<DatabaseMessage> messageBatch)
        {
            if (messageBatch == null || messageBatch.Count == 0) return;

            bool throwException = false;

            SendingExceptions = new ConcurrentQueue<Exception>();

            ConfigureConnection();
            ConfigureChannels();
            PrepareProducerQueues(messageBatch);
            try
            {
                ProcessProducerQueues();
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
        private void PrepareProducerQueues(List<DatabaseMessage> messageBatch)
        {
            string[] recipients = null;
            string exchangeName = string.Empty;
            foreach (DatabaseMessage message in messageBatch)
            {
                recipients = GetRecipients(message);
                if (recipients.Length == 0) continue;

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
                }
            }
            ProducerQueuesList = ProducerQueues.ToList();
        }
        private void ProcessProducerQueues()
        {
            using (SendingCancellation = new CancellationTokenSource())
            {
                ParallelOptions options = new ParallelOptions()
                {
                    CancellationToken = SendingCancellation.Token,
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                };
                Parallel.For(0, Channels.Count, options, ProcessProducerQueues);
            }
        }
        private void ProcessProducerQueues(int channelId)
        {
            SendingChannel sendingChannel = Channels[channelId];

            int messagesSent = 0;
            int nextQueue = channelId;
            while (nextQueue < ProducerQueuesList.Count)
            {
                int messagesCount = 0;
                var item = ProducerQueuesList[nextQueue];

                if (item.Value.Count == 0)
                {
                    nextQueue += Channels.Count;
                    continue;
                }

                FileLogger.Log(LOG_TOKEN, $"Sending {item.Value.Count} messages to \"{item.Key}\" queue...");

                while (item.Value.TryDequeue(out DatabaseMessage message))
                {
                    if (SendingCancellation.IsCancellationRequested) return;

                    JsonDataTransferMessage dtm = ProduceDataTransferMessage(message);
                    string json = JsonSerializer.Serialize(dtm);
                    byte[] messageBytes = Encoding.UTF8.GetBytes(json);

                    try
                    {
                        sendingChannel.Channel.BasicPublish(item.Key, string.Empty, true, sendingChannel.Properties, messageBytes);
                        messagesSent++;
                        messagesCount++;
                    }
                    catch (Exception error)
                    {
                        SendingExceptions.Enqueue(error);
                        SendingCancellation.Cancel();
                        return;
                    }
                }

                FileLogger.Log(LOG_TOKEN, $"{messagesCount} messages have been sent to \"{item.Key}\" queue.");

                nextQueue += Channels.Count;
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
            }
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