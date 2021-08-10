using DaJet.Agent.Model;
using DaJet.Agent.Producer;
using DaJet.Agent.Service;
using DaJet.Agent.Service.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using RabbitMQ.Management.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DaJet.Agent.Test
{
    [TestClass]
    public class UnitTests
    {
        private IOptions<AppSettings> AppSettings { get; set; }
        private IOptions<MessageProducerSettings> Settings { get; set; }
        public UnitTests()
        {
            InitializeAppSettings();
            InitializeTestSettings();
        }
        private void InitializeAppSettings()
        {
            AppSettings settings = new AppSettings()
            {
                AppCatalog = "C:\\temp\\"
            };
            AppSettings = Options.Create(settings);
        }
        private void InitializeTestSettings()
        {
            MessageProducerSettings settings = new MessageProducerSettings()
            {
                MessageBrokerSettings = new MessageBrokerSettings()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest",
                    PortNumber = 5672
                }
            };
            Settings = Options.Create(settings);
        }

        [TestMethod] public void CreateExchangeAndQueue()
        {
            //string prefix = "–»¡";
            //string mainNode = "MAIN";
            //string[] rayNodes = new string[] { "N001", "N002" };
            //IMessageProducer producer = new MessageProducer(Settings);

            //for (int i = 0; i < rayNodes.Length; i++)
            //{
            //    producer.CreateQueue($"{prefix}.{mainNode}.{rayNodes[i]}");
            //    producer.CreateQueue($"{prefix}.{rayNodes[i]}.{mainNode}");
            //}
        }

        [TestMethod] public void UpdatePostgreSQL()
        {
            string connectionString = "Host=127.0.0.1;Port=5432;Database=test_node_2;Username=postgres;Password=postgres;";
            string newValue = "—Ô‡‚Ó˜ÌËÍ.ÕÓÏÂÌÍÎ‡ÚÛ‡";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            using (NpgsqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE _reference39 SET _fld139 = CAST(@messageType AS mvarchar) WHERE _code = 63750991169884;";
                command.CommandTimeout = 60; // seconds
                command.Parameters.AddWithValue("messageType", newValue.ToCharArray());

                connection.Open();
                int recordsAffected = command.ExecuteNonQuery();
            }
        }

        [TestMethod] public void SelectNodes()
        {
            IPubSubService service = new PubSubService(AppSettings);
            List<Node> list = service.SelectNodes();
            if (list.Count == 0)
            {
                Console.WriteLine("List of nodes is empty.");
                return;
            }
            foreach (Node node in list)
            {
                Console.WriteLine(string.Format("{0}. {1} ({2})", node.Id, node.Code, node.Description));
            }
        }
        [TestMethod] public void CreateNode()
        {
            IPubSubService service = new PubSubService(AppSettings);

            for (int i = 0; i < 5; i++)
            {
                Node node = new Node()
                {
                    Code = "Node" + i.ToString(),
                    Description = "Test node " + i.ToString()
                };
                service.CreateNode(node);
            }
        }
        [TestMethod] public void UpdateNode()
        {
            IPubSubService service = new PubSubService(AppSettings);

            List<Node> list = service.SelectNodes();

            foreach (Node node in list)
            {
                node.Description = "Node " + node.Id.ToString() + " update test";
                service.UpdateNode(node);
            }
        }
        [TestMethod] public void DeleteNode()
        {
            IPubSubService service = new PubSubService(AppSettings);

            Node node = new Node();
            for (int i = 0; i < 5; i++)
            {
                node.Id = i + 1;
                service.DeleteNode(node);
            }
        }
        
        [TestMethod] public async Task GetHostInfo()
        {
            IRabbitHttpManager manager = new RabbitHttpManager();

            string exchangeName = "–»¡.MAIN.N001";
            ExchangeInfo exchange = await manager.GetExchange(exchangeName);
            if (exchange == null)
            {
                Console.WriteLine($"Exchange \"{exchangeName}\" is not found.");
            }
            else
            {
                Console.WriteLine($"Exchange \"{exchangeName}\" is {(exchange.Durable ? "durable" : "transient")}");
            }

            //List<ExchangeInfo> list = await manager.GetExchanges();

            //List<ExchangeInfo> exchanges = list.Where(e => e.Name.StartsWith("–»¡")).ToList();
            //if (exchanges == null || exchanges.Count == 0)
            //{
            //    Console.WriteLine("Exchanges are not found.");
            //}
            //else
            //{
            //    foreach (ExchangeInfo exchange in exchanges)
            //    {
            //        Console.WriteLine($"Exchange of type {exchange.Type} \"{exchange.Name}\" ({(exchange.Durable ? "durable" : "transient")})");
            //    }
            //}
        }
        [TestMethod] public async Task CreateExchange()
        {
            IRabbitHttpManager manager = new RabbitHttpManager();
            await manager.CreateExchange("DISPATCHER-TEST");
        }
        [TestMethod] public async Task DeleteExchange()
        {
            IRabbitHttpManager manager = new RabbitHttpManager();
            await manager.DeleteExchange("DISPATCHER-TEST");
        }

        [TestMethod] public async Task GetQueues()
        {
            IRabbitHttpManager manager = new RabbitHttpManager();

            List<QueueInfo> list = await manager.GetQueues();

            List<QueueInfo> queues = list.Where(e => e.Name.StartsWith("–»¡")).ToList();
            if (queues == null || queues.Count == 0)
            {
                Console.WriteLine("Queues are not found.");
            }
            else
            {
                foreach (QueueInfo queue in queues)
                {
                    Console.WriteLine($"Queue \"{queue.Name}\" ({(queue.Durable ? "durable" : "transient")})");
                    
                    List<BindingInfo> bindings = await manager.GetBindings(queue);
                    foreach (BindingInfo binding in bindings)
                    {
                        Console.WriteLine($" - [{binding.Source}] -> [{binding.Destination}] ({binding.RoutingKey}) {binding.PropertiesKey}");
                    }
                }
            }
        }
        [TestMethod] public async Task GetQueueByName()
        {
            IRabbitHttpManager manager = new RabbitHttpManager();

            string queueName = "–»¡.MAIN.N001";
            QueueInfo queue = await manager.GetQueue(queueName);
            if (queue == null)
            {
                Console.WriteLine($"Queue \"{queueName}\" is not found.");
            }
            else
            {
                Console.WriteLine($"Queue \"{queueName}\" is {(queue.Durable ? "durable" : "transient")}");
            }
        }
        [TestMethod] public async Task CreateQueue()
        {
            string queueName = "TEST-CREATE-QUEUE";
            IRabbitHttpManager manager = new RabbitHttpManager();
            try
            {
                await manager.CreateQueue(queueName);
                Console.WriteLine($"Queue {queueName} created successfully.");
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        [TestMethod] public async Task DeleteQueue()
        {
            string queueName = "TEST-CREATE-QUEUE";
            IRabbitHttpManager manager = new RabbitHttpManager();
            try
            {
                await manager.DeleteQueue(queueName);
                Console.WriteLine($"Queue {queueName} deleted successfully.");
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        [TestMethod] public async Task CreateBinding()
        {
            IRabbitHttpManager manager = new RabbitHttpManager();

            string queueName = "–»¡.MAIN.N001";
            string exchangeName = "DISPATCHER-TEST";
            string routingKey = "N001";

            ExchangeInfo exchange = await manager.GetExchange(exchangeName);
            if (exchange == null)
            {
                Console.WriteLine($"Exchange \"{exchangeName}\" not found.");
                return;
            }

            QueueInfo queue = await manager.GetQueue(queueName);
            if (queue == null)
            {
                Console.WriteLine($"Queue \"{queueName}\" not found.");
                return;
            }

            await manager.CreateBinding(exchange, queue, routingKey);

            Console.WriteLine($"Binding from {exchange.Name} to {queue.Name} created successfully.");
        }
        [TestMethod] public async Task DeleteBinding()
        {
            IRabbitHttpManager manager = new RabbitHttpManager();

            string queueName = "–»¡.MAIN.N001";
            string exchangeName = "DISPATCHER-TEST";

            ExchangeInfo exchange = await manager.GetExchange(exchangeName);
            if (exchange == null)
            {
                Console.WriteLine($"Exchange \"{exchangeName}\" not found.");
                return;
            }

            QueueInfo queue = await manager.GetQueue(queueName);
            if (queue == null)
            {
                Console.WriteLine($"Queue \"{queueName}\" not found.");
                return;
            }

            List<BindingInfo> bindings = await manager.GetBindings(queue);
            bindings = bindings.Where(b => b.Source == exchange.Name).ToList();
            foreach (BindingInfo binding in bindings)
            {
                await manager.DeleteBinding(binding);
                Console.WriteLine($"Binding ({binding.RoutingKey}) from {exchange.Name} to {queue.Name} deleted successfully.");
            }
        }
    }
}