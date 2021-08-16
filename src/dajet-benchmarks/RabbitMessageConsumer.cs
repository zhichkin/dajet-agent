using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace DaJet.Benchmarks
{
    public sealed class RabbitMessageConsumer : IDisposable
    {
        private const string RmqQueue = "РИБ.N001.MAIN";
        private IModel RmqChannel;
        private IConnection RmqConnection;
        private EventingBasicConsumer RmqConsumer;
        private EventHandler<BasicDeliverEventArgs> ConsumeMessageHandler;
        private string ConsumerTag;
        public RabbitMessageConsumer(EventHandler<BasicDeliverEventArgs> consumeMessageHandler)
        {
            InitializeConnection();
            InitializeChannel();
            InitializeConsumer(consumeMessageHandler);
        }
        private void InitializeConnection()
        {
            IConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            RmqConnection = factory.CreateConnection();
        }
        private void InitializeChannel()
        {
            RmqChannel = RmqConnection.CreateModel();
            RmqChannel.BasicQos(0, 1, false);
        }
        private void InitializeConsumer(EventHandler<BasicDeliverEventArgs> consumeMessageHandler)
        {
            ConsumeMessageHandler = consumeMessageHandler;
            RmqConsumer = new EventingBasicConsumer(RmqChannel);
            RmqConsumer.Received += ConsumeMessageHandler;
            ConsumerTag = RmqChannel.BasicConsume(RmqQueue, false, RmqConsumer);
        }
        public void Dispose()
        {
            RmqChannel.BasicCancel(ConsumerTag);
            RmqConsumer.Received -= ConsumeMessageHandler;

            RmqChannel.Dispose();
            RmqConnection.Dispose();
        }
    }
}