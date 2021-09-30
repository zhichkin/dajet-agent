using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using Microsoft.Data.SqlClient;
using Microsoft.IO;
using RabbitMQ.Client;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.Json;

namespace DaJet.Benchmarks
{
    [MemoryDiagnoser]
    [Config(typeof(Config))]
    public class SqlStreamingBenchmark
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                //AddJob(Job.Dry.WithGcServer(true).WithGcForce(true).WithId("ServerForce"));
                AddJob(Job.Dry.WithGcServer(true).WithGcForce(false).WithId("Server"));
                //AddJob(Job.Dry.WithGcServer(false).WithGcForce(true).WithId("Workstation"));
                //AddJob(Job.Dry.WithGcServer(false).WithGcForce(false).WithId("WorkstationForce"));
            }
        }

        private const string ConnectionString = "Data Source=zhichkin;Initial Catalog=cerberus;Integrated Security=True";
        private const string SelectMessagesScript = "SELECT TOP 1000 _Fld8900 AS [ТипОперации], _Fld8898 AS [ТипСообщения], _Fld8899 AS [ТелоСообщения] FROM _InfoRg8895 WITH (ROWLOCK, READPAST) ORDER BY _Fld8896 ASC, _Fld8897 ASC;";
        private const string SelectMaxMessageSizeScript = "SELECT MAX(DATALENGTH(T._Fld8899)) FROM (SELECT TOP 1000 _Fld8899 FROM _InfoRg8895) AS T;";

        // WITH CTE AS
        // (
        //    SELECT TOP 1000 _Fld8899 FROM _InfoRg8895
        // )
        // SELECT MAX(DATALENGTH(_Fld8899)) FROM CTE;

        private readonly RecyclableMemoryStreamManager StreamPool = new RecyclableMemoryStreamManager();

        [Params(1,5,10)] public int Iterations;

        [Benchmark(Description = "Data Streaming (two array pools)")] public void BenchmarkDataStreaming0()
        {
            for (int i = 0; i < Iterations; i++)
            {
                SelectDatabaseMessagesWithArrayPool();
            }
        }
        [Benchmark(Description = "Data Streaming (one array pool)")] public void BenchmarkDataStreaming1()
        {
            for (int i = 0; i < Iterations; i++)
            {
                SelectDatabaseMessagesWithArrayPoolForEncoding();
            }
        }
        [Benchmark(Description = "Data Streaming (no array pools)")] public void BenchmarkDataStreaming2()
        {
            for (int i = 0; i < Iterations; i++)
            {
                SelectDatabaseMessagesNoArrayPool();
            }
        }
        private IConnection GetRabbitMQConnection()
        {
            IConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost",
                VirtualHost = "/",
                UserName = "guest",
                Password = "guest",
                Port = 5672
            };
            return factory.CreateConnection();
        }
        private void SelectDatabaseMessagesWithArrayPool()
        {
            using (IConnection RmqConnection = GetRabbitMQConnection())
            {
                using (IModel channel = RmqConnection.CreateModel())
                {
                    channel.ConfirmSelect();

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.AppId = "TEST";
                    properties.ContentEncoding = "UTF-8";
                    properties.ContentType = "application/json";
                    properties.DeliveryMode = 2; // persistent
                    properties.Type = "Справочник.Партии";

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandTimeout = 10;
                            command.CommandType = CommandType.Text;
                            command.CommandText = SelectMessagesScript;

                            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess))
                            {
                                int bufferSize = 4096;
                                char[] charBuffer = ArrayPool<char>.Shared.Rent(bufferSize);
                                byte[] byteBuffer = ArrayPool<byte>.Shared.Rent(bufferSize);

                                while (reader.Read())
                                {
                                    long dataIndex = 0;
                                    long charsRead = 0;
                                    int bufferIndex = 0;
                                    int bytesEncoded = 0;
                                    do
                                    {
                                        charsRead = reader.GetChars(2, dataIndex, charBuffer, bufferIndex, bufferSize);

                                        if (charsRead > 0)
                                        {
                                            bytesEncoded = Encoding.UTF8.GetBytes(charBuffer, 0, (int)charsRead, byteBuffer, 0);

                                            ReadOnlyMemory<byte> messageBody = new ReadOnlyMemory<byte>(byteBuffer, 0, bytesEncoded);

                                            channel.BasicPublish("dajet-exchange", "РегистрСведений.Тестовый", properties, messageBody);

                                            dataIndex += charsRead;
                                            bufferIndex += (int)charsRead;
                                        }
                                    }
                                    while (charsRead == bufferSize);
                                }
                                reader.Close();

                                ArrayPool<char>.Shared.Return(charBuffer);
                                ArrayPool<byte>.Shared.Return(byteBuffer);
                            }
                        }
                    }
                    bool confirmed = channel.WaitForConfirms(TimeSpan.FromSeconds(1), out bool timedOut);
                }
            }
        }
        private void SelectDatabaseMessagesWithArrayPoolForEncoding()
        {
            using (IConnection RmqConnection = GetRabbitMQConnection())
            {
                using (IModel channel = RmqConnection.CreateModel())
                {
                    channel.ConfirmSelect();

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.AppId = "TEST";
                    properties.ContentEncoding = "UTF-8";
                    properties.ContentType = "application/json";
                    properties.DeliveryMode = 2; // persistent
                    properties.Type = "Справочник.Партии";

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandTimeout = 10;
                            command.CommandType = CommandType.Text;
                            command.CommandText = SelectMessagesScript;

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int bufferSize = 4096;
                                byte[] byteBuffer = ArrayPool<byte>.Shared.Rent(bufferSize);

                                while (reader.Read())
                                {
                                    string message = reader.GetString(2);

                                    int bytesEncoded = Encoding.UTF8.GetBytes(message, 0, message.Length, byteBuffer, 0);

                                    ReadOnlyMemory<byte> messageBody = new ReadOnlyMemory<byte>(byteBuffer, 0, bytesEncoded);

                                    channel.BasicPublish("dajet-exchange", "РегистрСведений.Тестовый", properties, messageBody);
                                }
                                reader.Close();

                                ArrayPool<byte>.Shared.Return(byteBuffer);
                            }
                        }
                    }
                    bool confirmed = channel.WaitForConfirms(TimeSpan.FromSeconds(1), out bool timedOut);
                }
            }
        }
        private void SelectDatabaseMessagesNoArrayPool()
        {
            using (IConnection RmqConnection = GetRabbitMQConnection())
            {
                using (IModel channel = RmqConnection.CreateModel())
                {
                    channel.ConfirmSelect();

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.AppId = "TEST";
                    properties.ContentEncoding = "UTF-8";
                    properties.ContentType = "application/json";
                    properties.DeliveryMode = 2; // persistent
                    properties.Type = "Справочник.Партии";

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandTimeout = 10;
                            command.CommandType = CommandType.Text;
                            command.CommandText = SelectMessagesScript;

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string message = reader.GetString(2);

                                    byte[] messageBody = Encoding.UTF8.GetBytes(message);

                                    channel.BasicPublish("dajet-exchange", "РегистрСведений.Тестовый", properties, messageBody);
                                }
                                reader.Close();
                            }
                        }
                    }
                    bool confirmed = channel.WaitForConfirms(TimeSpan.FromSeconds(1), out bool timedOut);
                }
            }
        }
    }
}