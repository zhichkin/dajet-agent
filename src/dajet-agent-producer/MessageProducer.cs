using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;

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
            return $"dajet.{sender}.{recipient}";
        }
        private string[] GetRecipients(DatabaseMessage message)
        {
            return message.Recipients.Split(',');
        }
        public void Send(DatabaseMessage message)
        {
            InitializeChannel();

            string[] recipients = GetRecipients(message);
            foreach (string recipient in recipients)
            {
                string exchangeName = CreateExchangeName(message.Sender, recipient);
                byte[] messageBytes = Encoding.UTF8.GetBytes(message.MessageBody);
                Channel.BasicPublish(exchangeName, string.Empty, Properties, messageBytes);
            }
            bool confirmed = Channel.WaitForConfirms(TimeSpan.FromSeconds(Settings.MessageBrokerSettings.ConfirmationTimeout));
            if (!confirmed)
            {
                throw new OperationCanceledException(PUBLISHER_CONFIRMATION_ERROR_MESSAGE);
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