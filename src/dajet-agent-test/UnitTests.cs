using DaJet.Agent.Model;
using DaJet.Agent.Producer;
using DaJet.Agent.Service;
using DaJet.Agent.Service.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

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
            //string prefix = "РИБ";
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
            string newValue = "Справочник.Номенклатура";

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
    }
}