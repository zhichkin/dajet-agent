using DaJet.Metadata;
using DaJet.Metadata.Model;
using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace DaJet.Database.Adapter
{
    public interface IDatabaseConfigurator
    {
        string ConnectionString { get; }
        DatabaseProvider DatabaseProvider { get; }
        IDatabaseConfigurator UseConnectionString(string connectionString);
        IDatabaseConfigurator UseDatabaseProvider(DatabaseProvider databaseProvider);
        void ConfigureIncomingQueue(ApplicationObject queue);
        void ConfigureOutgoingQueue(ApplicationObject queue);
    }
    public sealed class DatabaseConfigurator : IDatabaseConfigurator
    {
        #region "Incoming queue scripts"

        private const string MS_CREATE_INCOMING_SEQUENCE_SCRIPT =
            "IF NOT EXISTS(SELECT 1 FROM sys.sequences WHERE name = N'DaJetIncomingQueueSequence') " +
            "BEGIN CREATE SEQUENCE DaJetIncomingQueueSequence AS numeric(19,0) START WITH 1 INCREMENT BY 1; END;";

        private const string MS_DROP_INCOMING_TRIGGER_SCRIPT =
            "IF OBJECT_ID('DaJetIncomingQueue_INSTEAD_OF_INSERT', 'TR') IS NOT NULL " +
            "DROP TRIGGER DaJetIncomingQueue_INSTEAD_OF_INSERT";

        private const string MS_CREATE_INCOMING_TRIGGER_SCRIPT =
            "CREATE TRIGGER DaJetIncomingQueue_INSTEAD_OF_INSERT ON {TABLE_NAME} INSTEAD OF INSERT AS " +
            "IF EXISTS(SELECT 1 FROM inserted WHERE {НомерСообщения} IS NULL OR {НомерСообщения} = 0) " +
            "BEGIN INSERT {TABLE_NAME} " +
            "({НомерСообщения}, {Отправитель}, {ТипОперации}, {ТипСообщения}, {ТелоСообщения}, {ДатаВремя}, {ОписаниеОшибки}, {КоличествоОшибок}) " +
            "SELECT NEXT VALUE FOR DaJetIncomingQueueSequence, " +
            "i.{Отправитель}, i.{ТипОперации}, i.{ТипСообщения}, i.{ТелоСообщения}, i.{ДатаВремя}, i.{ОписаниеОшибки}, i.{КоличествоОшибок} " +
            "FROM inserted AS i; END " +
            "ELSE BEGIN INSERT {TABLE_NAME} " +
            "({НомерСообщения}, {Отправитель}, {ТипОперации}, {ТипСообщения}, {ТелоСообщения}, {ДатаВремя}, {ОписаниеОшибки}, {КоличествоОшибок}) " +
            "SELECT i.{НомерСообщения}, i.{Отправитель}, i.{ТипОперации}, i.{ТипСообщения}, i.{ТелоСообщения}, i.{ДатаВремя}, i.{ОписаниеОшибки}, i.{КоличествоОшибок} " +
            "FROM inserted AS i; END;";

        #endregion

        //private const string MS_CREATE_OUTGOING_SEQUENCE_SCRIPT =
        //    "IF NOT EXISTS(SELECT 1 FROM sys.sequences WHERE name = N'DaJetOutgoingQueueSequence') " +
        //    "BEGIN CREATE SEQUENCE DaJetOutgoingQueueSequence AS numeric(19,0) START WITH 1 INCREMENT BY 1; END;";

        public string ConnectionString { get; private set; }
        public DatabaseProvider DatabaseProvider { get; private set; } = DatabaseProvider.SQLServer;
        public IDatabaseConfigurator UseConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
            return this;
        }
        public IDatabaseConfigurator UseDatabaseProvider(DatabaseProvider databaseProvider)
        {
            DatabaseProvider = databaseProvider;
            return this;
        }

        private DatabaseField GetDatabaseField(ApplicationObject queue, string propertyName)
        {
            foreach (MetadataProperty property in queue.Properties)
            {
                if (property.Name != propertyName)
                {
                    continue;
                }

                if (property.Fields.Count > 0)
                {
                    return property.Fields[0];
                }
            }
            return null;
        }

        public void ConfigureIncomingQueue(ApplicationObject queue)
        {
            CreateIncomingQueueSequence();
            DropIfExistsIncomingQueueTrigger();
            CreateIncomingQueueTrigger(queue);
        }
        private void CreateIncomingQueueSequence()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = MS_CREATE_INCOMING_SEQUENCE_SCRIPT;
                command.CommandTimeout = 10; // seconds

                connection.Open();
                int affected = command.ExecuteNonQuery();
            }
        }
        private void DropIfExistsIncomingQueueTrigger()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = MS_DROP_INCOMING_TRIGGER_SCRIPT;
                command.CommandTimeout = 10; // seconds

                connection.Open();
                int affected = command.ExecuteNonQuery();
            }
        }
        private void CreateIncomingQueueTrigger(ApplicationObject queue)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = ConfigureCreateIncomingQueueTriggerScript(queue);
                command.CommandTimeout = 10; // seconds

                connection.Open();
                int affected = command.ExecuteNonQuery();
            }
        }
        private string ConfigureCreateIncomingQueueTriggerScript(ApplicationObject queue)
        {
            string script = MS_CREATE_INCOMING_TRIGGER_SCRIPT
                .Replace("{TABLE_NAME}", queue.TableName);

            foreach (PropertyInfo info in typeof(DatabaseIncomingMessage).GetProperties())
            {
                ColumnAttribute column = info.GetCustomAttribute<ColumnAttribute>();
                if (column == null)
                {
                    continue;
                }

                DatabaseField field = GetDatabaseField(queue, column.Name);
                if (field == null)
                {
                    continue;
                }

                script = script.Replace($"{{{column.Name}}}", field.Name);
            }

            return script;
        }

        public void ConfigureOutgoingQueue(ApplicationObject queue)
        {
            throw new NotImplementedException();
        }
    }
}