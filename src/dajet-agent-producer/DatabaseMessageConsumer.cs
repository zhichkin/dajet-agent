using DaJet.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Linq;
using System.Text;

namespace DaJet.Agent.Producer
{
    public interface IDatabaseMessageConsumer
    {
        int ConsumeMessages(int count, out string errorMessage);
        int AwaitNotification(int timeout, out string errorMessage);
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
        private void RollbackTransaction(SqlTransaction transaction, SqlDataReader reader, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (transaction == null) return;

            try
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                transaction.Rollback();
            }
            catch (Exception error)
            {
                errorMessage = ExceptionHelper.GetErrorText(error);
            }
        }
        private void DisposeDatabaseResources(SqlConnection connection, SqlDataReader reader, SqlCommand command)
        {
            if (reader != null)
            {
                if (!reader.IsClosed && reader.HasRows)
                {
                    command.Cancel();
                }
                reader.Dispose();
            }
            if (command != null) command.Dispose();
            if (connection != null) connection.Dispose();
        }

        public int ConsumeMessages(int messageCount, out string errorMessage)
        {
            int messagesRecevied = 0;
            errorMessage = string.Empty;
            {
                SqlDataReader reader = null;
                SqlTransaction transaction = null;
                SqlConnection connection = new SqlConnection(Settings.DatabaseSettings.ConnectionString);

                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = ReceiveMessagesScript(messageCount);
                command.CommandTimeout = 60; // seconds

                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    command.Transaction = transaction;

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        MessageProducer.Send(CreateDatabaseMessage(reader));
                    }
                    reader.Close();
                    messagesRecevied = reader.RecordsAffected;

                    transaction.Commit();
                }
                catch (Exception error)
                {
                    errorMessage = ExceptionHelper.GetErrorText(error);

                    RollbackTransaction(transaction, reader, out string rollbackError);

                    if (!string.IsNullOrEmpty(rollbackError))
                    {
                        errorMessage += Environment.NewLine + rollbackError;
                    }
                }
                finally
                {
                    DisposeDatabaseResources(connection, reader, command);
                }
            }
            return messagesRecevied;
        }
        private DatabaseMessage CreateDatabaseMessage(SqlDataReader reader)
        {
            DatabaseMessage message = new DatabaseMessage();
            message.Code = reader.IsDBNull(0) ? 0 : (long)reader.GetDecimal(0);
            message.Uuid = reader.IsDBNull(1) ? Guid.Empty : new Guid(reader.GetSqlBinary(1).Value);
            message.Version = reader.IsDBNull(2) ? 0 : BitConverter.ToInt64((byte[])reader[2]);
            message.DateTimeStamp = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3);
            message.Sender = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
            message.Recipients = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
            message.OperationType = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
            message.MessageType = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
            message.MessageBody = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
            return message;
        }
        private string ReceiveMessagesScript(int messageCount)
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

        public int AwaitNotification(int timeout, out string errorMessage)
        {
            int resultCode = 0;
            errorMessage = string.Empty;
            {
                SqlDataReader reader = null;
                SqlTransaction transaction = null;
                SqlConnection connection = new SqlConnection(Settings.DatabaseSettings.ConnectionString);

                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = AwaitNotificationScript(timeout);
                command.CommandTimeout = timeout / 1000 + 10;

                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    command.Transaction = transaction;

                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        resultCode = 0; // notification received successfully
                        byte[] message_body = new byte[16];
                        Guid dialog_handle = reader.GetGuid("dialog_handle");
                        string message_type = reader.GetString("message_type");
                        long readBytes = reader.GetBytes("message_body", 0, message_body, 0, 16);
                    }
                    else
                    {
                        resultCode = 2; // no notification received
                    }
                    reader.Close();

                    transaction.Commit();
                }
                catch (Exception error)
                {
                    resultCode = 1; // notifications are not supported by database

                    errorMessage = ExceptionHelper.GetErrorText(error);

                    RollbackTransaction(transaction, reader, out string rollbackError);

                    if (!string.IsNullOrEmpty(rollbackError))
                    {
                        errorMessage += Environment.NewLine + rollbackError;
                    }
                }
                finally
                {
                    DisposeDatabaseResources(connection, reader, command);
                }
            }
            return resultCode;
        }
        private string AwaitNotificationScript(int timeout)
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("WAITFOR (RECEIVE TOP (1)");
            script.AppendLine("conversation_handle AS [dialog_handle],");
            script.AppendLine("message_type_name   AS [message_type],");
            script.AppendLine("message_body        AS [message_body]");
            script.AppendLine($"FROM [dajet-exchange-export-queue]), TIMEOUT {timeout};");
            return script.ToString();
        }
    }
}