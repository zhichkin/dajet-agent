using DaJet.Metadata;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
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
        private MessageProducerSettings Settings { get; set; }
        private IMessageProducer MessageProducer { get; set; }
        public DatabaseMessageConsumer(IOptions<MessageProducerSettings> options, IMessageProducer messageProducer)
        {
            Settings = options.Value;
            MessageProducer = messageProducer;
        }
        
        public int ConsumeMessages(int messageCount)
        {
            return ConsumeDatabaseMessages(messageCount);
        }
        private DbConnection CreateDbConnection()
        {
            if (Settings.DatabaseSettings.DatabaseProvider == DatabaseProviders.SQLServer)
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
                    command.CommandText = ConsumeDatabaseMessagesScript(messageCount);
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
                        if (batch.Count > 0) MessageProducer.Publish(batch);

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
        private string ConsumeDatabaseMessagesScript(int messageCount)
        {
            if (Settings.DatabaseSettings.DatabaseProvider == DatabaseProviders.SQLServer)
            {
                return MS_ReceiveMessagesScript(messageCount);
            }
            return PG_ReceiveMessagesScript(messageCount);
        }
        private string MS_ReceiveMessagesScript(int messageCount)
        {
            DatabaseQueue queue = Settings.DatabaseSettings.DatabaseQueue;
            string tableName = queue.TableName;
            string field1 = queue.Fields.Where(f => f.Property == "ДатаВремя").FirstOrDefault()?.Name;
            string field2 = queue.Fields.Where(f => f.Property == "Отправитель").FirstOrDefault()?.Name;
            string field3 = queue.Fields.Where(f => f.Property == "Получатели").FirstOrDefault()?.Name;
            string field4 = queue.Fields.Where(f => f.Property == "ТипОперации").FirstOrDefault()?.Name;
            string field5 = queue.Fields.Where(f => f.Property == "ТипСообщения").FirstOrDefault()?.Name;
            string field6 = queue.Fields.Where(f => f.Property == "ТелоСообщения").FirstOrDefault()?.Name;

            StringBuilder script = new StringBuilder();
            script.AppendLine("WITH [CTE] AS");
            script.AppendLine("(");
            script.AppendLine($"SELECT TOP({messageCount})");
            script.AppendLine("[_Code] AS [Код],");
            script.AppendLine("[_IDRRef] AS [Ссылка],");
            script.AppendLine("[_Version] AS [ВерсияДанных],");
            script.AppendLine($"[{field1}] AS [ДатаВремя],");
            script.AppendLine($"[{field2}] AS [Отправитель],");
            script.AppendLine($"[{field3}] AS [Получатели],");
            script.AppendLine($"[{field4}] AS [ТипОперации],");
            script.AppendLine($"[{field5}] AS [ТипСообщения],");
            script.AppendLine($"[{field6}] AS [ТелоСообщения]");
            script.AppendLine("FROM");
            script.AppendLine($"[{tableName}] WITH (ROWLOCK)");
            script.AppendLine("ORDER BY");
            script.AppendLine("[_Code] ASC, [_IDRRef] ASC");
            script.AppendLine(")");
            script.AppendLine("DELETE [CTE]");
            script.AppendLine("OUTPUT deleted.[Код], deleted.[Ссылка], deleted.[ВерсияДанных],");
            script.AppendLine("deleted.[ДатаВремя], deleted.[Отправитель], deleted.[Получатели],");
            script.AppendLine("deleted.[ТипОперации], deleted.[ТипСообщения], deleted.[ТелоСообщения];");
            return script.ToString();
        }
        private string PG_ReceiveMessagesScript(int messageCount)
        {
            DatabaseQueue queue = Settings.DatabaseSettings.DatabaseQueue;
            string tableName = queue.TableName;
            string field1 = queue.Fields.Where(f => f.Property == "ДатаВремя").FirstOrDefault()?.Name;
            string field2 = queue.Fields.Where(f => f.Property == "Отправитель").FirstOrDefault()?.Name;
            string field3 = queue.Fields.Where(f => f.Property == "Получатели").FirstOrDefault()?.Name;
            string field4 = queue.Fields.Where(f => f.Property == "ТипОперации").FirstOrDefault()?.Name;
            string field5 = queue.Fields.Where(f => f.Property == "ТипСообщения").FirstOrDefault()?.Name;
            string field6 = queue.Fields.Where(f => f.Property == "ТелоСообщения").FirstOrDefault()?.Name;

            //DELETE FROM _reference39 WHERE _idrref IN
            //(SELECT _idrref FROM _reference39 ORDER BY _code ASC, _idrref ASC LIMIT 10)
            //RETURNING
            //_code as "Код", _idrref as "Ссылка", _version as "ВерсияДанных",
            //_fld135 as "ДатаВремя", _fld136 as "Отправитель",
            //_fld137 as "Получатели", _fld138 as "ТипОперации",
            //_fld139 as "ТипСообщения", _fld140 as "ТелоСообщения";

            StringBuilder script = new StringBuilder();
            script.AppendLine("WITH cte AS");
            script.AppendLine($"(SELECT _code, _idrref FROM {tableName} ORDER BY _code ASC, _idrref ASC LIMIT {messageCount})");
            script.AppendLine($"DELETE FROM {tableName} t USING cte");
            script.AppendLine("WHERE t._code = cte._code AND t._idrref = cte._idrref");
            script.AppendLine("RETURNING");
            script.AppendLine("t._code AS \"Код\", t._idrref AS \"Ссылка\", t._version AS \"ВерсияДанных\",");
            script.AppendLine($"t.{field1} AS \"ДатаВремя\", CAST(t.{field2} AS varchar) AS \"Отправитель\",");
            script.AppendLine($"CAST(t.{field3} AS varchar) AS \"Получатели\", CAST(t.{field4} AS varchar) AS \"ТипОперации\",");
            script.AppendLine($"CAST(t.{field5} AS varchar) AS \"ТипСообщения\", CAST(t.{field6} AS text) AS \"ТелоСообщения\";");
            return script.ToString();
        }
        private DatabaseMessage CreateDatabaseMessage(DbDataReader reader)
        {
            DatabaseMessage message = new DatabaseMessage();
            message.Code = reader.IsDBNull(0) ? 0 : (long)reader.GetDecimal(0);
            message.Uuid = reader.IsDBNull(1) ? Guid.Empty : new Guid((byte[])reader[1]);
            if (Settings.DatabaseSettings.DatabaseProvider == DatabaseProviders.SQLServer)
            {
                message.Version = reader.IsDBNull(2) ? 0 : BitConverter.ToInt64((byte[])reader[2]);
            }
            else
            {
                message.Version = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
            }
            message.DateTimeStamp = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3);
            message.Sender = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
            message.Recipients = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
            message.OperationType = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
            message.MessageType = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
            message.MessageBody = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
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