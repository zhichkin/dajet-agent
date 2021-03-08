using DaJet.Utilities;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Text;
using System.Text.Json;

namespace DaJet.Agent.Producer
{
    public interface IMessageProducer : IDisposable
    {
        void CreateQueue(string name);
        void Send(DatabaseMessage message);
    }
    public sealed class MessageProducer: IMessageProducer
    {
        private const string PUBLISHER_CONFIRMATION_ERROR_MESSAGE = "The sending of the message has not been confirmed. Check the availability of the message broker.";

        private IModel Channel { get; set; }
        private IConnection Connection { get; set; }
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
            return message.Recipients.Split(',');
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

            string exchangeName = string.Empty;
            string[] recipients = GetRecipients(message);
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
                        FileLogger.Log(ExceptionHelper.GetErrorText(rabbitError));
                    }
                    else
                    {
                        throw;
                    }
                }
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
    }
}