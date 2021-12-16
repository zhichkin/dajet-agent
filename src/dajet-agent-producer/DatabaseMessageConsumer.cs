using DaJet.Database.Adapter;
using DaJet.Metadata;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DaJet.Agent.Producer
{
    public interface IDatabaseMessageConsumer
    {
        int ConsumeMessages(int count);
        void AwaitNotification(int timeout);
    }
    public sealed class DatabaseMessageConsumer : IDatabaseMessageConsumer
    {
        private IServiceProvider Services { get; set; }
        private MessageProducerSettings Settings { get; set; }
        private IMessageProducer MessageProducer { get; set; }
        private string OutgoingQueueSelectScript { get; set; }
        private int YearOffset { get; } = 0;
        public DatabaseMessageConsumer(IServiceProvider serviceProvider, IOptions<MessageProducerSettings> options)
        {
            Settings = options.Value;
            Services = serviceProvider;
            MessageProducer = Services.GetService<IMessageProducer>();
            IDatabaseConfigurator configurator = Services.GetService<IDatabaseConfigurator>();
            OutgoingQueueSelectScript = configurator.OutgoingQueueSelectScript;
            YearOffset = configurator.YearOffset;
        }

        public int ConsumeMessages(int messageCount)
        {
            return ConsumeDatabaseMessages(messageCount);
        }
        private DbConnection CreateDbConnection()
        {
            if (Settings.DatabaseSettings.DatabaseProvider == DatabaseProvider.SQLServer)
            {
                return new SqlConnection(Settings.DatabaseSettings.ConnectionString);
            }
            return new NpgsqlConnection(Settings.DatabaseSettings.ConnectionString);
        }
        private int ConsumeDatabaseMessages(int messageCount)
        {
            int messagesRecevied = 0;
            using (DbConnection connection = CreateDbConnection())
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                using (DbCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.Transaction = transaction;
                    command.CommandType = CommandType.Text;
                    command.CommandText = OutgoingQueueSelectScript;
                    command.CommandTimeout = 60; // seconds

                    try
                    {
                        List<DatabaseMessage> batch = new List<DatabaseMessage>();
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                batch.Add(CreateDatabaseMessage(reader));
                            }
                            reader.Close();
                            messagesRecevied = reader.RecordsAffected;
                        }

                        if (batch.Count > 0)
                        {
                            MessageProducer.Publish(batch);
                        }

                        transaction.Commit();
                    }
                    catch (Exception error)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        finally
                        {
                            throw error;
                        }
                    }
                }
            }
            return messagesRecevied;
        }
        private DatabaseMessage CreateDatabaseMessage(DbDataReader reader)
        {
            DatabaseMessage message = new DatabaseMessage();
            message.Code = reader.IsDBNull(0) ? 0 : (long)reader.GetDecimal("МоментВремени");
            message.Uuid = reader.IsDBNull(1) ? Guid.Empty : new Guid((byte[])reader["Идентификатор"]);
            message.DateTimeStamp = reader.IsDBNull(2) ? DateTime.MinValue : reader.GetDateTime("ДатаВремя").AddYears(-YearOffset);
            message.Sender = reader.IsDBNull(3) ? string.Empty : reader.GetString("Отправитель");
            message.Recipients = reader.IsDBNull(4) ? string.Empty : reader.GetString("Получатели");
            message.OperationType = reader.IsDBNull(5) ? string.Empty : reader.GetString("ТипОперации");
            message.MessageType = reader.IsDBNull(6) ? string.Empty : reader.GetString("ТипСообщения");
            message.MessageBody = reader.IsDBNull(7) ? string.Empty : reader.GetString("ТелоСообщения");
            return message;
        }

        public void AwaitNotification(int timeout)
        {
            using (DbConnection connection = CreateDbConnection())
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                using (DbCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.Transaction = transaction;
                    command.CommandType = CommandType.Text;
                    command.CommandText = AwaitNotificationScript(timeout);
                    command.CommandTimeout = timeout / 1000 + (timeout == 0 ? 0 : 10); // seconds

                    try
                    {
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] message_body = new byte[16];
                                Guid dialog_handle = reader.GetGuid("dialog_handle");
                                string message_type = reader.GetString("message_type");
                                long readBytes = reader.GetBytes("message_body", 0, message_body, 0, 16);
                            }
                            reader.Close();
                        }
                        transaction.Commit();
                    }
                    catch (Exception error)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        finally
                        {
                            throw error;
                        }
                    }
                }
            }
        }
        private string AwaitNotificationScript(int timeout)
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("WAITFOR (RECEIVE TOP (1)");
            script.AppendLine("conversation_handle AS [dialog_handle],");
            script.AppendLine("message_type_name   AS [message_type],");
            script.AppendLine("message_body        AS [message_body]");
            script.AppendLine($"FROM [{Settings.DatabaseSettings.NotificationQueueName}]), TIMEOUT {timeout};");
            return script.ToString();
        }
    }
}