using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;

namespace DaJet.Agent.Producer
{
    public interface IMessageProducer : IDisposable
    {
        void CreateQueue();
        void SendMessage(string messageBody);
        void SendMessage(string messageType, string messageBody);
    }
    public sealed class MessageProducer: IMessageProducer
    {
        private const string DEFAULT_EXCHANGE_NAME = "exchange";
        private const string PUBLISHER_CONFIRMATION_ERROR_MESSAGE = "The sending of the message has not been confirmed. Check the availability of the message broker.";

        private IModel Channel { get; set; }
        private IConnection Connection { get; set; }
        private IBasicProperties Properties { get; set; }
        private MessageProducerSettings Settings { get; set; }
        public MessageProducer(IOptions<MessageProducerSettings> options)
        {
            Settings = options.Value;
        }
        private bool QueueExists()
        {
            bool exists = true;
            try
            {
                QueueDeclareOk queue = Channel.QueueDeclarePassive(Settings.QueueName);
            }
            catch
            {
                exists = false;
            }
            return exists;
        }
        public void CreateQueue()
        {
            InitializeChannel();

            Channel.ExchangeDeclare(Settings.ExchangeName, ExchangeType.Direct, true, false, null);
            QueueDeclareOk queue = Channel.QueueDeclare(Settings.QueueName, true, false, false, null);
            if (queue == null)
            {
                throw new InvalidOperationException($"Creating \"{Settings.QueueName}\" queue failed.");
            }
            Channel.QueueBind(Settings.QueueName, Settings.ExchangeName, Settings.RoutingKey, null);
        }

        private IConnection CreateConnection()
        {
            IConnectionFactory factory = new ConnectionFactory()
            {
                HostName = Settings.HostName,
                UserName = Settings.UserName,
                Password = Settings.Password,
                Port = Settings.PortNumber
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

        private string CreateExchangeName(string routingKey)
        {
            if (string.IsNullOrWhiteSpace(routingKey))
            {
                return Settings.ExchangeName;
            }
            return Settings.ExchangeName.Replace(DEFAULT_EXCHANGE_NAME, routingKey);
        }

        public void SendMessage(string messageBody)
        {
            SendMessage(string.Empty, messageBody);
        }
        public void SendMessage(string messageType, string messageBody)
        {
            InitializeChannel();

            _ = Settings.MessageTypeRouting.TryGetValue(messageType, out string routingKey);

            string exchangeName = CreateExchangeName(routingKey);
            byte[] message = Encoding.UTF8.GetBytes(messageBody);

            //Channel.ConfirmSelect();
            Channel.BasicPublish(exchangeName, Settings.RoutingKey, Properties, message);
            bool confirmed = Channel.WaitForConfirms(TimeSpan.FromSeconds(Settings.ConfirmationTimeout));
            //if (Channel.NextPublishSeqNo == 2) { }
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