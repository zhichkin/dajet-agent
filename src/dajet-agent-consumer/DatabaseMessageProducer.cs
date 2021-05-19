﻿using DaJet.Metadata;
using DaJet.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DaJet.Agent.Consumer
{
    public interface IDatabaseMessageProducer
    {
        bool InsertMessage(DatabaseMessage message);
        DatabaseMessage ProduceMessage(JsonDataTransferMessage message);
    }
    public sealed class DatabaseMessageProducer : IDatabaseMessageProducer
    {
        private const string LOG_TOKEN = "P-DB";
        private IServiceProvider Services { get; set; }
        private MessageConsumerSettings Settings { get; set; }
        public DatabaseMessageProducer(IServiceProvider serviceProvider, IOptions<MessageConsumerSettings> options)
        {
            Settings = options.Value;
            Services = serviceProvider;
        }
        private void DisposeDatabaseResources(DbConnection connection, DbDataReader reader, DbCommand command)
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
        public DatabaseMessage ProduceMessage(JsonDataTransferMessage message)
        {
            DatabaseMessage dbm = new DatabaseMessage();
            dbm.Sender = message.Sender;
            if (message.Objects.Count > 0)
            {
                dbm.MessageType = message.Objects[0].Type;
                dbm.MessageBody = message.Objects[0].Body;
                dbm.OperationType = message.Objects[0].Operation;
            }
            return dbm;
        }
        public bool InsertMessage(DatabaseMessage message)
        {
            // TODO: use try-catch statement and handle exceptions thrown from using statements in this method !?
            if (Settings.DatabaseSettings.DatabaseProvider == DatabaseProviders.SQLServer)
            {
                return SQLServer_InsertMessage(message);
            }
            return PostgreSQL_InsertMessage(message);
        }

        #region "Microsoft SQL Server"

        public bool SQLServer_InsertMessage(DatabaseMessage message)
        {
            int recordsAffected = 0;
            {
                SqlConnection connection = new SqlConnection(Settings.DatabaseSettings.ConnectionString);
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = SQLServer_InsertMessageScript();
                command.CommandTimeout = 60; // seconds

                command.Parameters.AddWithValue("p1", message.Code);
                command.Parameters.AddWithValue("p2", message.Uuid);
                command.Parameters.AddWithValue("p3", message.DateTimeStamp);
                command.Parameters.AddWithValue("p4", message.Sender);
                command.Parameters.AddWithValue("p5", message.OperationType);
                command.Parameters.AddWithValue("p6", message.MessageType);
                command.Parameters.AddWithValue("p7", message.MessageBody);
                command.Parameters.AddWithValue("p8", message.ErrorCount);
                command.Parameters.AddWithValue("p9", message.ErrorDescription);

                try
                {
                    connection.Open();
                    recordsAffected = command.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error));
                }
                finally
                {
                    // TODO: replace with using statement and handle exceptions outside the method !?
                    DisposeDatabaseResources(connection, null, command);
                }
            }
            if (Settings.DebugMode)
            {
                throw new Exception("Failed to insert message to database. Records affected = " + recordsAffected.ToString());
            }
            return (recordsAffected != 0);
        }
        private string SQLServer_InsertMessageScript()
        {
            DatabaseQueue queue = Settings.DatabaseSettings.DatabaseQueue;
            string tableName = queue.TableName;
            string field1 = queue.Fields.Where(f => f.Property == "МоментВремени").FirstOrDefault()?.Name;
            string field2 = queue.Fields.Where(f => f.Property == "Идентификатор").FirstOrDefault()?.Name;
            string field3 = queue.Fields.Where(f => f.Property == "ДатаВремя").FirstOrDefault()?.Name;
            string field4 = queue.Fields.Where(f => f.Property == "Отправитель").FirstOrDefault()?.Name;
            string field5 = queue.Fields.Where(f => f.Property == "ТипОперации").FirstOrDefault()?.Name;
            string field6 = queue.Fields.Where(f => f.Property == "ТипСообщения").FirstOrDefault()?.Name;
            string field7 = queue.Fields.Where(f => f.Property == "ТелоСообщения").FirstOrDefault()?.Name;
            string field8 = queue.Fields.Where(f => f.Property == "КоличествоОшибок").FirstOrDefault()?.Name;
            string field9 = queue.Fields.Where(f => f.Property == "ОписаниеОшибки").FirstOrDefault()?.Name;

            StringBuilder script = new StringBuilder();
            script.AppendLine($"INSERT [{tableName}]");
            script.AppendLine($"([{field1}], [{field2}], [{field3}], [{field4}], [{field5}], [{field6}], [{field7}], [{field8}], [{field9}])");
            script.AppendLine("VALUES (@p1, CAST(@p2 AS binary(16)), @p3, @p4, @p5, @p6, @p7, @p8, @p9);");
            return script.ToString();
        }

        #endregion

        #region "PostgreSQL"

        public bool PostgreSQL_InsertMessage(DatabaseMessage message)
        {
            int recordsAffected = 0;
            {
                NpgsqlConnection connection = new NpgsqlConnection(Settings.DatabaseSettings.ConnectionString);
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = PostgreSQL_InsertMessageScript();
                command.CommandTimeout = 60; // seconds

                command.Parameters.AddWithValue("p1", message.Code);
                command.Parameters.AddWithValue("p2", message.Uuid.ToByteArray());
                command.Parameters.AddWithValue("p3", message.DateTimeStamp);
                command.Parameters.AddWithValue("p4", message.Sender);
                command.Parameters.AddWithValue("p5", message.OperationType);
                command.Parameters.AddWithValue("p6", message.MessageType);
                command.Parameters.AddWithValue("p7", message.MessageBody);
                command.Parameters.AddWithValue("p8", message.ErrorCount);
                command.Parameters.AddWithValue("p9", message.ErrorDescription);

                try
                {
                    connection.Open();
                    recordsAffected = command.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    // TODO: replace with using statement and handle exceptions outside the method !?
                    FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error));
                }
                finally
                {
                    DisposeDatabaseResources(connection, null, command);
                }
            }
            if (Settings.DebugMode)
            {
                throw new Exception("Failed to insert message to database. Records affected = " + recordsAffected.ToString());
            }
            return (recordsAffected != 0);
        }
        private string PostgreSQL_InsertMessageScript()
        {
            DatabaseQueue queue = Settings.DatabaseSettings.DatabaseQueue;
            string tableName = queue.TableName;
            string field1 = queue.Fields.Where(f => f.Property == "МоментВремени").FirstOrDefault()?.Name;
            string field2 = queue.Fields.Where(f => f.Property == "Идентификатор").FirstOrDefault()?.Name;
            string field3 = queue.Fields.Where(f => f.Property == "ДатаВремя").FirstOrDefault()?.Name;
            string field4 = queue.Fields.Where(f => f.Property == "Отправитель").FirstOrDefault()?.Name;
            string field5 = queue.Fields.Where(f => f.Property == "ТипОперации").FirstOrDefault()?.Name;
            string field6 = queue.Fields.Where(f => f.Property == "ТипСообщения").FirstOrDefault()?.Name;
            string field7 = queue.Fields.Where(f => f.Property == "ТелоСообщения").FirstOrDefault()?.Name;
            string field8 = queue.Fields.Where(f => f.Property == "КоличествоОшибок").FirstOrDefault()?.Name;
            string field9 = queue.Fields.Where(f => f.Property == "ОписаниеОшибки").FirstOrDefault()?.Name;

            StringBuilder script = new StringBuilder();
            script.AppendLine($"INSERT INTO {tableName}");
            script.AppendLine($"({field1}, {field2}, {field3}, {field4}, {field5}, {field6}, {field7}, {field8}, {field9})");
            script.AppendLine("VALUES (@p1, @p2, @p3, CAST(@p4 AS mvarchar), CAST(@p5 AS mvarchar), ");
            script.AppendLine("CAST(@p6 AS mvarchar), CAST(@p7 AS mvarchar), @p8, CAST(@p9 AS mvarchar));");
            return script.ToString();
        }

        #endregion
    }
}