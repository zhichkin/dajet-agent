using DaJet.Metadata;
using DaJet.Metadata.Model;
using Microsoft.Data.SqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
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
        void DropIncomingQueueSequence();
        bool IncomingQueueSequenceExists();
        void DropIncomingQueueTrigger(ApplicationObject queue);
        void ConfigureIncomingQueue(ApplicationObject queue);
        void DropOutgoingQueueSequence();
        bool OutgoingQueueSequenceExists();
        void ConfigureOutgoingQueue(ApplicationObject queue);
        ApplicationObject FindMetadataObjectByName(InfoBase infoBase, string metadataName);
    }
    public sealed class DatabaseConfigurator : IDatabaseConfigurator
    {
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

        private DbConnection GetDbConnection()
        {
            if (DatabaseProvider == DatabaseProvider.SQLServer)
            {
                return new SqlConnection(ConnectionString);
            }
            return new NpgsqlConnection(ConnectionString);
        }
        private T ExecuteScalar<T>(string script)
        {
            T result = default(T);
            using (DbConnection connection = GetDbConnection())
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 10; // seconds
                command.CommandText = script;
                
                connection.Open();
                object value = command.ExecuteScalar();
                if (value != null)
                {
                    result = (T)value;
                }
            }
            return result;
        }
        private void ExecuteNonQuery(string script)
        {
            using (DbConnection connection = GetDbConnection())
            using (DbCommand command = connection.CreateCommand())
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
            using (DbConnection connection = GetDbConnection())
            {
                connection.Open();

                using (DbTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                using (DbCommand command = connection.CreateCommand())
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

        #region "Outgoing Queue Setup"

        #region "MS outgoing queue setup scripts"

        private const string MS_OUTGOING_SEQUENCE_EXISTS_SCRIPT = "SELECT 1 FROM sys.sequences WHERE name = 'DaJetOutgoingQueueSequence';";

        private const string MS_DROP_OUTGOING_SEQUENCE_SCRIPT =
            "IF EXISTS(SELECT 1 FROM sys.sequences WHERE name = N'DaJetOutgoingQueueSequence') " +
            "BEGIN DROP SEQUENCE DaJetOutgoingQueueSequence; END;";

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

        #endregion

        #region "PG outgoing queue setup scripts"

        private const string PG_OUTGOING_SEQUENCE_EXISTS_SCRIPT =
            "SELECT 1 FROM information_schema.sequences WHERE LOWER(sequence_name) = LOWER('DaJetOutgoingQueueSequence');";

        private const string PG_DROP_OUTGOING_SEQUENCE_SCRIPT = "DROP SEQUENCE IF EXISTS DaJetOutgoingQueueSequence;";

        private const string PG_CREATE_OUTGOING_SEQUENCE_SCRIPT =
            "CREATE SEQUENCE IF NOT EXISTS DaJetOutgoingQueueSequence AS bigint INCREMENT BY 1 START WITH 1 CACHE 1;";

        private const string PG_ENUMERATE_OUTGOING_QUEUE_SCRIPT =
            "LOCK TABLE {TABLE_NAME} IN ACCESS EXCLUSIVE MODE; " +
            "WITH cte AS (SELECT {МоментВремени}, {Идентификатор}, nextval('DaJetOutgoingQueueSequence') AS msgno " +
            "FROM {TABLE_NAME} ORDER BY {МоментВремени} ASC, {Идентификатор} ASC) " +
            "UPDATE {TABLE_NAME} SET {МоментВремени} = CAST(cte.msgno AS numeric(19, 0)) " +
            "FROM cte WHERE {TABLE_NAME}.{МоментВремени} = cte.{МоментВремени} AND {TABLE_NAME}.{Идентификатор} = cte.{Идентификатор};";

        private const string PG_CREATE_OUTGOING_FUNCTION_SCRIPT =
            "CREATE OR REPLACE FUNCTION DaJetOutgoingQueue_before_insert_function() RETURNS trigger AS $$ BEGIN " +
            "NEW.{МоментВремени} := CAST(nextval('DaJetOutgoingQueueSequence') AS numeric(19,0)); RETURN NEW; END $$ LANGUAGE 'plpgsql';";

        private const string PG_DROP_OUTGOING_TRIGGER_SCRIPT = "DROP TRIGGER IF EXISTS DaJetOutgoingQueue_before_insert_trigger ON {TABLE_NAME};";

        private const string PG_CREATE_OUTGOING_TRIGGER_SCRIPT =
            "CREATE TRIGGER DaJetOutgoingQueue_before_insert_trigger BEFORE INSERT ON {TABLE_NAME} FOR EACH ROW " +
            "EXECUTE PROCEDURE DaJetOutgoingQueue_before_insert_function();";

        #endregion

        public bool OutgoingQueueSequenceExists()
        {
            if (DatabaseProvider == DatabaseProvider.SQLServer)
            {
                return MS_OutgoingQueueSequenceExists();
            }
            else
            {
                return PG_OutgoingQueueSequenceExists();
            }
        }
        private bool MS_OutgoingQueueSequenceExists()
        {
            int result = ExecuteScalar<int>(MS_OUTGOING_SEQUENCE_EXISTS_SCRIPT);
            return (result == 1);
        }
        private bool PG_OutgoingQueueSequenceExists()
        {
            int result = ExecuteScalar<int>(PG_OUTGOING_SEQUENCE_EXISTS_SCRIPT);
            return (result == 1);
        }

        public void DropOutgoingQueueSequence()
        {
            if (DatabaseProvider == DatabaseProvider.SQLServer)
            {
                ExecuteNonQuery(MS_DROP_OUTGOING_SEQUENCE_SCRIPT);
            }
            else
            {
                ExecuteNonQuery(PG_DROP_OUTGOING_SEQUENCE_SCRIPT);
            }
        }

        public void ConfigureOutgoingQueue(ApplicationObject queue)
        {
            Type template = typeof(DatabaseOutgoingMessage);

            if (DatabaseProvider == DatabaseProvider.SQLServer)
            {
                MS_ConfigureOutgoingQueue(queue, template);
            }
            else
            {
                PG_ConfigureOutgoingQueue(queue, template);
            }
        }
        private void MS_ConfigureOutgoingQueue(ApplicationObject queue, Type template)
        {
            List<string> scripts = new List<string>();

            scripts.Add(MS_CREATE_OUTGOING_SEQUENCE_SCRIPT);
            scripts.Add(ConfigureDatabaseScript(MS_ENUMERATE_OUTGOING_QUEUE_SCRIPT, template, queue));
            scripts.Add(ConfigureDatabaseScript(MS_DROP_OUTGOING_ENUMERATION_TABLE, template, queue));
            scripts.Add(ConfigureDatabaseScript(MS_DROP_OUTGOING_TRIGGER_SCRIPT, template, queue));
            scripts.Add(ConfigureDatabaseScript(MS_CREATE_OUTGOING_TRIGGER_SCRIPT, template, queue));
            scripts.Add(ConfigureDatabaseScript(MS_ENABLE_OUTGOING_TRIGGER_SCRIPT, template, queue));

            TxExecuteNonQuery(scripts);
        }
        private void PG_ConfigureOutgoingQueue(ApplicationObject queue, Type template)
        {
            List<string> scripts = new List<string>();

            scripts.Add(PG_CREATE_OUTGOING_SEQUENCE_SCRIPT);
            scripts.Add(ConfigureDatabaseScript(PG_ENUMERATE_OUTGOING_QUEUE_SCRIPT, template, queue));
            scripts.Add(ConfigureDatabaseScript(PG_CREATE_OUTGOING_FUNCTION_SCRIPT, template, queue));
            scripts.Add(ConfigureDatabaseScript(PG_DROP_OUTGOING_TRIGGER_SCRIPT, template, queue));
            scripts.Add(ConfigureDatabaseScript(PG_CREATE_OUTGOING_TRIGGER_SCRIPT, template, queue));

            TxExecuteNonQuery(scripts);
        }

        #endregion

        #region "Incoming Queue Setup"

        #region "MS incoming queue setup scripts"

        private const string MS_INCOMING_SEQUENCE_EXISTS_SCRIPT = "SELECT 1 FROM sys.sequences WHERE name = 'DaJetIncomingQueueSequence';";

        private const string MS_DROP_INCOMING_SEQUENCE_SCRIPT =
            "IF EXISTS(SELECT 1 FROM sys.sequences WHERE name = N'DaJetIncomingQueueSequence') " +
            "BEGIN DROP SEQUENCE DaJetIncomingQueueSequence; END;";

        private const string MS_CREATE_INCOMING_SEQUENCE_SCRIPT =
            "IF NOT EXISTS(SELECT 1 FROM sys.sequences WHERE name = N'DaJetIncomingQueueSequence') " +
            "BEGIN CREATE SEQUENCE DaJetIncomingQueueSequence AS numeric(19,0) START WITH 1 INCREMENT BY 1; END;";

        private const string MS_DROP_INCOMING_TRIGGER_SCRIPT =
            "IF OBJECT_ID('DaJetIncomingQueue_INSTEAD_OF_INSERT', 'TR') IS NOT NULL " +
            "BEGIN DROP TRIGGER DaJetIncomingQueue_INSTEAD_OF_INSERT; END;";

        private const string MS_CREATE_INCOMING_TRIGGER_SCRIPT =
            "CREATE TRIGGER DaJetIncomingQueue_INSTEAD_OF_INSERT ON {TABLE_NAME} INSTEAD OF INSERT NOT FOR REPLICATION AS " +
            "INSERT {TABLE_NAME} " +
            "({МоментВремени}, {Идентификатор}, {Отправитель}, {ТипОперации}, {ТипСообщения}, {ТелоСообщения}, {ДатаВремя}, {ОписаниеОшибки}, {КоличествоОшибок}) " +
            "SELECT NEXT VALUE FOR DaJetIncomingQueueSequence, " +
            "i.{Идентификатор}, i.{Отправитель}, i.{ТипОперации}, i.{ТипСообщения}, i.{ТелоСообщения}, i.{ДатаВремя}, i.{ОписаниеОшибки}, i.{КоличествоОшибок} " +
            "FROM inserted AS i;";

        private const string MS_ENABLE_INCOMING_TRIGGER_SCRIPT = "ENABLE TRIGGER DaJetIncomingQueue_INSTEAD_OF_INSERT ON {TABLE_NAME};";

        private const string MS_ENUMERATE_INCOMING_QUEUE_SCRIPT =
            "SELECT {МоментВремени} AS [МоментВремени], {Идентификатор} AS [Идентификатор], " +
            "NEXT VALUE FOR DaJetIncomingQueueSequence OVER(ORDER BY {МоментВремени} ASC, {Идентификатор} ASC) AS [НомерСообщения] " +
            "INTO #{TABLE_NAME}_EnumCopy " +
            "FROM {TABLE_NAME} WITH (TABLOCKX, HOLDLOCK); " +
            "UPDATE T SET T.{МоментВремени} = C.[НомерСообщения] FROM {TABLE_NAME} AS T " +
            "INNER JOIN #{TABLE_NAME}_EnumCopy AS C ON T.{МоментВремени} = C.[МоментВремени] AND T.{Идентификатор} = C.[Идентификатор];";

        private const string MS_DROP_INCOMING_ENUMERATION_TABLE = "DROP TABLE #{TABLE_NAME}_EnumCopy;";

        #endregion

        #region "PG incoming queue setup scripts"

        private const string PG_INCOMING_SEQUENCE_EXISTS_SCRIPT =
            "SELECT 1 FROM information_schema.sequences WHERE LOWER(sequence_name) = LOWER('DaJetIncomingQueueSequence');";

        private const string PG_DROP_INCOMING_SEQUENCE_SCRIPT = "DROP SEQUENCE IF EXISTS DaJetIncomingQueueSequence;";

        private const string PG_CREATE_INCOMING_SEQUENCE_SCRIPT =
            "CREATE SEQUENCE IF NOT EXISTS DaJetIncomingQueueSequence AS bigint INCREMENT BY 1 START WITH 1 CACHE 1;";

        private const string PG_ENUMERATE_INCOMING_QUEUE_SCRIPT =
            "LOCK TABLE {TABLE_NAME} IN ACCESS EXCLUSIVE MODE; " +
            "WITH cte AS (SELECT {МоментВремени}, {Идентификатор}, nextval('DaJetIncomingQueueSequence') AS msgno " +
            "FROM {TABLE_NAME} ORDER BY {МоментВремени} ASC, {Идентификатор} ASC) " +
            "UPDATE {TABLE_NAME} SET {МоментВремени} = CAST(cte.msgno AS numeric(19, 0)) " +
            "FROM cte WHERE {TABLE_NAME}.{МоментВремени} = cte.{МоментВремени} AND {TABLE_NAME}.{Идентификатор} = cte.{Идентификатор};";

        private const string PG_DROP_INCOMING_FUNCTION_SCRIPT = "DROP FUNCTION IF EXISTS DaJetIncomingQueue_before_insert_function;";

        private const string PG_CREATE_INCOMING_FUNCTION_SCRIPT =
            "CREATE OR REPLACE FUNCTION DaJetIncomingQueue_before_insert_function() RETURNS trigger AS $$ BEGIN " +
            "NEW.{МоментВремени} := CAST(nextval('DaJetIncomingQueueSequence') AS numeric(19,0)); RETURN NEW; END $$ LANGUAGE 'plpgsql';";

        private const string PG_DROP_INCOMING_TRIGGER_SCRIPT = "DROP TRIGGER IF EXISTS DaJetIncomingQueue_before_insert_trigger ON {TABLE_NAME};";

        private const string PG_CREATE_INCOMING_TRIGGER_SCRIPT =
            "CREATE TRIGGER DaJetIncomingQueue_before_insert_trigger BEFORE INSERT ON {TABLE_NAME} FOR EACH ROW " +
            "EXECUTE PROCEDURE DaJetIncomingQueue_before_insert_function();";

        #endregion

        public bool IncomingQueueSequenceExists()
        {
            if (DatabaseProvider == DatabaseProvider.SQLServer)
            {
                return MS_IncomingQueueSequenceExists();
            }
            else
            {
                return PG_IncomingQueueSequenceExists();
            }
        }
        private bool MS_IncomingQueueSequenceExists()
        {
            int result = ExecuteScalar<int>(MS_INCOMING_SEQUENCE_EXISTS_SCRIPT);
            return (result == 1);
        }
        private bool PG_IncomingQueueSequenceExists()
        {
            int result = ExecuteScalar<int>(PG_INCOMING_SEQUENCE_EXISTS_SCRIPT);
            return (result == 1);
        }

        public void DropIncomingQueueSequence()
        {
            if (DatabaseProvider == DatabaseProvider.SQLServer)
            {
                ExecuteNonQuery(MS_DROP_INCOMING_SEQUENCE_SCRIPT);
            }
            else
            {
                ExecuteNonQuery(PG_DROP_INCOMING_SEQUENCE_SCRIPT);
            }
        }

        public void DropIncomingQueueTrigger(ApplicationObject queue)
        {
            if (DatabaseProvider == DatabaseProvider.SQLServer)
            {
                ExecuteNonQuery(MS_DROP_INCOMING_TRIGGER_SCRIPT);
            }
            else
            {
                ExecuteNonQuery(ConfigureDatabaseScript(PG_DROP_INCOMING_TRIGGER_SCRIPT, typeof(DatabaseIncomingMessage), queue));
                ExecuteNonQuery(PG_DROP_INCOMING_FUNCTION_SCRIPT);
            }
        }

        public void ConfigureIncomingQueue(ApplicationObject queue)
        {
            Type template = typeof(DatabaseIncomingMessage);

            if (DatabaseProvider == DatabaseProvider.SQLServer)
            {
                MS_ConfigureIncomingQueue(queue, template);
            }
            else
            {
                PG_ConfigureIncomingQueue(queue, template);
            }
        }
        private void MS_ConfigureIncomingQueue(ApplicationObject queue, Type template)
        {
            List<string> scripts = new List<string>();

            scripts.Add(MS_CREATE_INCOMING_SEQUENCE_SCRIPT);
            scripts.Add(ConfigureDatabaseScript(MS_ENUMERATE_INCOMING_QUEUE_SCRIPT, template, queue));
            scripts.Add(ConfigureDatabaseScript(MS_DROP_INCOMING_ENUMERATION_TABLE, template, queue));

            TxExecuteNonQuery(scripts);
        }
        private void PG_ConfigureIncomingQueue(ApplicationObject queue, Type template)
        {
            List<string> scripts = new List<string>();

            scripts.Add(PG_CREATE_INCOMING_SEQUENCE_SCRIPT);
            scripts.Add(ConfigureDatabaseScript(PG_ENUMERATE_INCOMING_QUEUE_SCRIPT, template, queue));

            TxExecuteNonQuery(scripts);
        }

        #endregion
    }
}