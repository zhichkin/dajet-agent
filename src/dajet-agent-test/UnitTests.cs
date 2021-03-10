using DaJet.Agent.Producer;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DaJet.Agent.Test
{
    [TestClass]
    public class UnitTests
    {
        private IOptions<MessageProducerSettings> Settings { get; set; }
        public UnitTests()
        {
            InitializeTestSettings();
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
            string prefix = "РИБ";
            string mainNode = "MAIN";
            string[] rayNodes = new string[] { "N001", "N002" };
            IMessageProducer producer = new MessageProducer(Settings);

            for (int i = 0; i < rayNodes.Length; i++)
            {
                producer.CreateQueue($"{prefix}.{mainNode}.{rayNodes[i]}");
                producer.CreateQueue($"{prefix}.{rayNodes[i]}.{mainNode}");
            }
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
    }
}