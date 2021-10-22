using DaJet.Metadata;
using DaJet.Metadata.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
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
        ApplicationObject FindMetadataObjectByName(InfoBase infoBase, string metadataName);
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

        public ApplicationObject FindMetadataObjectByName(InfoBase infoBase, string metadataName)
        {
            string[] names = metadataName.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (names.Length != 2)
            {
                //throw new ArgumentException($"Bad metadata object name format: {nameof(metadataName)}");
                return null;
            }
            string baseType = names[0];
            string typeName = names[1];

            ApplicationObject metaObject = null;
            Dictionary<Guid, ApplicationObject> collection = null;

            if (baseType == "Справочник") collection = infoBase.Catalogs;
            else if (baseType == "Документ") collection = infoBase.Documents;
            else if (baseType == "ПланОбмена") collection = infoBase.Publications;
            else if (baseType == "РегистрСведений") collection = infoBase.InformationRegisters;
            else if (baseType == "РегистрНакопления") collection = infoBase.AccumulationRegisters;

            if (collection == null)
            {
                //throw new ArgumentException($"Collection \"{baseType}\" for metadata object \"{metadataName}\" is not found.");
                return null;
            }

            metaObject = collection.Values.Where(o => o.Name == typeName).FirstOrDefault();

            //if (metaObject == null)
            //{
            //    throw new ArgumentException($"Metadata object \"{metadataName}\" is not found.");
            //}

            return metaObject;
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

        private string ConfigureDatabaseScript(string scriptTemplate, Type queueTemplate, ApplicationObject queue)
        {
            string script = scriptTemplate.Replace("{TABLE_NAME}", queue.TableName);

            foreach (PropertyInfo info in queueTemplate.GetProperties())
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

        public void ConfigureIncomingQueue(ApplicationObject queue)
        {
            // TODO

            //CreateIncomingQueueSequence();
            //DropIfExistsIncomingQueueTrigger();
            //CreateIncomingQueueTrigger(queue);
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

        #region "Outgoing Queue Setup"

        private const string MS_CREATE_OUTGOING_SEQUENCE_SCRIPT =
            "IF NOT EXISTS(SELECT 1 FROM sys.sequences WHERE name = 'DaJetOutgoingQueueSequence') " +
            "BEGIN CREATE SEQUENCE DaJetOutgoingQueueSequence AS numeric(19,0) START WITH 1 INCREMENT BY 1; END;";

        private const string MS_DROP_OUTGOING_TRIGGER_SCRIPT =
            "IF OBJECT_ID('DaJetOutgoingQueue_INSTEAD_OF_INSERT', 'TR') IS NOT NULL " +
            "BEGIN DROP TRIGGER DaJetOutgoingQueue_INSTEAD_OF_INSERT END;";

        private const string MS_CREATE_OUTGOING_TRIGGER_SCRIPT =
            "CREATE TRIGGER DaJetOutgoingQueue_INSTEAD_OF_INSERT ON {TABLE_NAME} INSTEAD OF INSERT NOT FOR REPLICATION AS " +
            "INSERT {TABLE_NAME} " +
            "({МоментВремени}, {Идентификатор}, {Отправитель}, {Получатели}, {ТипСообщения}, {ТелоСообщения}, {ДатаВремя}, {ТипОперации}) " +
            "SELECT NEXT VALUE FOR DaJetOutgoingQueueSequence, " +
            "i.{Идентификатор}, i.{Отправитель}, i.{Получатели}, i.{ТипСообщения}, i.{ТелоСообщения}, i.{ДатаВремя}, i.{ТипОперации} " +
            "FROM inserted AS i;";

        private const string MS_ENABLE_OUTGOING_TRIGGER_SCRIPT = "ENABLE TRIGGER DaJetOutgoingQueue_INSTEAD_OF_INSERT ON {TABLE_NAME};";

        private const string MS_ENUMERATE_OUTGOING_QUEUE_SCRIPT =
            "SELECT {МоментВремени} AS [МоментВремени], {Идентификатор} AS [Идентификатор], " +
            "NEXT VALUE FOR DaJetOutgoingQueueSequence OVER(ORDER BY {МоментВремени} ASC, {Идентификатор} ASC) AS [НомерСообщения] " +
            "INTO #{TABLE_NAME}_EnumCopy " +
            "FROM {TABLE_NAME} WITH (TABLOCKX, HOLDLOCK); " +
            "UPDATE T SET T.{МоментВремени} = C.[НомерСообщения] FROM {TABLE_NAME} AS T " +
            "INNER JOIN #{TABLE_NAME}_EnumCopy AS C ON T.{МоментВремени} = C.[МоментВремени] AND T.{Идентификатор} = C.[Идентификатор];";

        private const string MS_DROP_OUTGOING_ENUMERATION_TABLE = "DROP TABLE #{TABLE_NAME}_EnumCopy;";

        private void ExecuteNonQuery(string script)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 10; // seconds
                command.CommandText = script;

                connection.Open();
                _ = command.ExecuteNonQuery();
            }
        }
        private void TxExecuteNonQuery(List<string> scripts)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.Transaction = transaction;
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 30; // seconds

                    try
                    {
                        foreach (string script in scripts)
                        {
                            command.CommandText = script;
                            _ = command.ExecuteNonQuery();
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

        public void ConfigureOutgoingQueue(ApplicationObject queue)
        {
            Type queueTemplate = typeof(DatabaseOutgoingMessage);

            ExecuteNonQuery(MS_CREATE_OUTGOING_SEQUENCE_SCRIPT);

            List<string> scripts = new List<string>();
            scripts.Add(ConfigureDatabaseScript(MS_ENUMERATE_OUTGOING_QUEUE_SCRIPT, queueTemplate, queue));
            scripts.Add(ConfigureDatabaseScript(MS_DROP_OUTGOING_ENUMERATION_TABLE, queueTemplate, queue));
            scripts.Add(ConfigureDatabaseScript(MS_DROP_OUTGOING_TRIGGER_SCRIPT, queueTemplate, queue));
            scripts.Add(ConfigureDatabaseScript(MS_CREATE_OUTGOING_TRIGGER_SCRIPT, queueTemplate, queue));
            scripts.Add(ConfigureDatabaseScript(MS_ENABLE_OUTGOING_TRIGGER_SCRIPT, queueTemplate, queue));
            
            TxExecuteNonQuery(scripts);
        }

        #endregion
    }
}