using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;

namespace test_consumers
{
    internal class StartConsumerParameters
    {
        internal string Exchange;
        internal IConnection Connection;
    }
    public sealed class Consumer
    {
        private int resetCount = 0;
        private IConnection connection;
        private ConcurrentDictionary<string, string> consumerTags = new ConcurrentDictionary<string, string>();
        public void StartConsuming()
        {
            IConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };
            connection = factory.CreateConnection();

            string[] exchanges = new string[] { "РИБ.MAIN.N001", "РИБ.MAIN.N002" };

            foreach (string exchange in exchanges)
            {
                StartConsumerTask(exchange);
            }
        }
        private void StartConsumerTask(string exchange)
        {
            _ = Task.Factory.StartNew(StartNewConsumer, new StartConsumerParameters()
            {
                Exchange = exchange,
                Connection = connection
            });
        }
        private void StartNewConsumer(object parameters)
        {
            if (!(parameters is StartConsumerParameters p)) return;

            IModel channel = p.Connection.CreateModel();
            channel.BasicQos(0, 1, false);

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += ProcessMessage;
            string tag = channel.BasicConsume(p.Exchange, false, consumer);
            _ = consumerTags.TryAdd(tag, p.Exchange);
        }
        private void ResetConsumer(string consumerTag, EventingBasicConsumer consumer)
        {
            resetCount++;

            consumer.Model.Dispose();
            consumer.Received -= ProcessMessage;
            _ = consumerTags.TryRemove(consumerTag, out string exchange);

            Task.Delay(15000).Wait();

            //consumer.Model.BasicCancel(consumerTag);
            //consumer.Model.BasicNack(args.DeliveryTag, false, false);

            StartConsumerTask(exchange);
        }
        private void ProcessMessage(object sender, BasicDeliverEventArgs args)
        {
            if (!(sender is EventingBasicConsumer consumer)) return;

            _ = consumerTags.TryGetValue(args.ConsumerTag, out string exchange);

            byte[] body = args.Body.ToArray();
            string messageBody = Encoding.UTF8.GetString(body);
            
            if (exchange == "РИБ.MAIN.N001")
            {
                Console.WriteLine(exchange + " says (" + DateTime.Now.ToString("HH:mm:ss") + "): " + messageBody);
                consumer.Model.BasicAck(args.DeliveryTag, false);
            }
            else
            {
                if (resetCount < 1)
                {
                    Console.WriteLine(exchange + " (" + DateTime.Now.ToString("HH:mm:ss") + ") reset is invoked.");
                    ResetConsumer(args.ConsumerTag, consumer);
                }
                else
                {
                    Console.WriteLine(exchange + " says (" + DateTime.Now.ToString("HH:mm:ss") + "): " + messageBody);
                    consumer.Model.BasicAck(args.DeliveryTag, false);
                    resetCount = 0;
                }
            }
        }
    }
}