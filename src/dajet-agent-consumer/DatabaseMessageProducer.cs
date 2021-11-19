using DaJet.Database.Adapter;
using DaJet.Metadata;
using DaJet.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Data;
using System.Data.Common;

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
        private string IncomingQueueInsertScript { get; set; }
        public DatabaseMessageProducer(IServiceProvider serviceProvider, IOptions<MessageConsumerSettings> options)
        {
            Settings = options.Value;
            Services = serviceProvider;
            IDatabaseConfigurator configurator = Services.GetService<IDatabaseConfigurator>();
            IncomingQueueInsertScript = configurator.IncomingQueueInsertScript;
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
            DatabaseMessage dbm = new DatabaseMessage()
            {
                Sender = message.Sender
            };
            if (Settings.DatabaseSettings.DatabaseProvider == DatabaseProvider.PostgreSQL)
            {
                // SELECT ofset FROM _yearoffset - returns zero rows
                dbm.DateTimeStamp = DateTime.Now;
            }
            else
            {
                // настройка ИБ 1С - смещение дат SELECT [Offset] FROM [_YearOffset]
                dbm.DateTimeStamp = DateTime.Now.AddYears(2000);
            }
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
            if (Settings.DatabaseSettings.DatabaseProvider == DatabaseProvider.SQLServer)
            {
                try
                {
                    return MS_InsertMessage(message);
                }
                catch (Exception error)
                {
                    FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error));
                    return false;
                }
            }
            return PG_InsertMessage(message);
        }
        public bool MS_InsertMessage(DatabaseMessage message)
        {
            int recordsAffected = 0;
            using(SqlConnection connection = new SqlConnection(Settings.DatabaseSettings.ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = IncomingQueueInsertScript;
                command.CommandTimeout = 60; // seconds

                command.Parameters.AddWithValue("Идентификатор", message.Uuid);
                command.Parameters.AddWithValue("ДатаВремя", message.DateTimeStamp);
                command.Parameters.AddWithValue("Отправитель", message.Sender);
                command.Parameters.AddWithValue("ТипОперации", message.OperationType);
                command.Parameters.AddWithValue("ТипСообщения", message.MessageType);
                command.Parameters.AddWithValue("ТелоСообщения", message.MessageBody);
                command.Parameters.AddWithValue("КоличествоОшибок", message.ErrorCount);
                command.Parameters.AddWithValue("ОписаниеОшибки", message.ErrorDescription);

                connection.Open();
                recordsAffected = command.ExecuteNonQuery();
            }
            return (recordsAffected != 0);
        }
        public bool PG_InsertMessage(DatabaseMessage message)
        {
            int recordsAffected = 0;
            {
                NpgsqlConnection connection = new NpgsqlConnection(Settings.DatabaseSettings.ConnectionString);
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = IncomingQueueInsertScript;
                command.CommandTimeout = 60; // seconds

                command.Parameters.AddWithValue("Идентификатор", message.Uuid.ToByteArray());
                command.Parameters.AddWithValue("ДатаВремя", message.DateTimeStamp);
                command.Parameters.AddWithValue("Отправитель", message.Sender);
                command.Parameters.AddWithValue("ТипОперации", message.OperationType);
                command.Parameters.AddWithValue("ТипСообщения", message.MessageType);
                command.Parameters.AddWithValue("ТелоСообщения", message.MessageBody);
                command.Parameters.AddWithValue("КоличествоОшибок", message.ErrorCount);
                command.Parameters.AddWithValue("ОписаниеОшибки", message.ErrorDescription);

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
    }
}