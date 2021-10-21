using DaJet.Metadata;
using DaJet.Metadata.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DaJet.Database.Adapter
{
    public interface IDatabaseMessageProducer
    {
        string ConnectionString { get; }
        DatabaseProvider DatabaseProvider { get; }
        IDatabaseMessageProducer UseConnectionString(string connectionString);
        IDatabaseMessageProducer UseDatabaseProvider(DatabaseProvider databaseProvider);
        void Initialize(InfoBase infoBase, string metadataName);
        string InsertMessageScript { get; }
        void InsertMessage(DatabaseIncomingMessage message);
    }
    public sealed class DatabaseMessageProducer : IDatabaseMessageProducer
    {
        private int _YearOffset;

        public string ConnectionString { get; private set; }
        public DatabaseProvider DatabaseProvider { get; private set; } = DatabaseProvider.SQLServer;
        public IDatabaseMessageProducer UseConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
            return this;
        }
        public IDatabaseMessageProducer UseDatabaseProvider(DatabaseProvider databaseProvider)
        {
            DatabaseProvider = databaseProvider;
            return this;
        }

        public string InsertMessageScript { get; private set; }

        public void Initialize(InfoBase infoBase, string metadataName)
        {
            _YearOffset = infoBase.YearOffset;

            ApplicationObject metaObject = FindMetadataObjectByName(infoBase, metadataName);

            if (DatabaseProvider == DatabaseProvider.SQLServer)
            {
                InsertMessageScript = Build_MS_InsertMessageScript(metaObject);
            }
            else
            {
                InsertMessageScript = Build_PG_InsertMessageScript(metaObject);
            }
        }
        private ApplicationObject FindMetadataObjectByName(InfoBase infoBase, string metadataName)
        {
            string[] names = metadataName.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (names.Length != 2)
            {
                throw new ArgumentException($"Bad metadata object name format: {nameof(metadataName)}");
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
                throw new ArgumentException($"Collection \"{baseType}\" for metadata object \"{metadataName}\" is not found.");
            }

            metaObject = collection.Values.Where(o => o.Name == typeName).FirstOrDefault();

            if (metaObject == null)
            {
                throw new ArgumentException($"Metadata object \"{metadataName}\" is not found.");
            }

            return metaObject;
        }
        private Dictionary<string, string> GetDatabaseInterface(ApplicationObject metaObject)
        {
            if (string.IsNullOrWhiteSpace(metaObject.TableName))
            {
                throw new Exception($"The metadata object \"{metaObject.Name}\" does not have a database table defined.");
            }

            Dictionary<string, string> fields = new Dictionary<string, string>();

            foreach (PropertyInfo info in typeof(DatabaseIncomingMessage).GetProperties())
            {
                if (info.GetCustomAttribute<NotMappedAttribute>() != null)
                {
                    continue;
                }

                ColumnAttribute column = info.GetCustomAttribute<ColumnAttribute>();
                if (column == null)
                {
                    throw new Exception($"The property \"{info.Name}\" does not have attribute Column applied.");
                }

                DatabaseField field = null;

                foreach (MetadataProperty property in metaObject.Properties)
                {
                    if (property.Fields.Count == 0)
                    {
                        throw new Exception($"The property \"{property.Name}\" does not have a database field defined.");
                    }
                    if (property.Fields.Count > 1)
                    {
                        throw new Exception($"The property \"{property.Name}\" has too many database fields defined.");
                    }
                    if (property.Name == column.Name)
                    {
                        field = property.Fields[0]; break;
                    }
                }

                if (field == null)
                {
                    throw new Exception($"The property \"{info.Name}\" does not match database interface.");
                }

                fields.Add(column.Name, field.Name);
            }

            return fields;
        }
        private string Build_MS_InsertMessageScript(ApplicationObject metaObject)
        {
            Dictionary<string, string> fields = GetDatabaseInterface(metaObject);

            int counter = 0;
            StringBuilder fieldNames = new StringBuilder();
            StringBuilder paramNames = new StringBuilder();
            foreach (KeyValuePair<string, string> field in fields)
            {
                counter++;

                paramNames.Append($"@{field.Key}");
                fieldNames.Append($"[{field.Value}]");
                
                if (counter != fields.Count)
                {
                    paramNames.Append($", ");
                    fieldNames.Append($", ");
                }
            }

            StringBuilder script = new StringBuilder();
            script.AppendLine($"INSERT [{metaObject.TableName}]");

            script.Append("(");
            script.Append(fieldNames.ToString());
            script.AppendLine(")");

            script.Append("VALUES (");
            script.Append(paramNames.ToString());
            script.Append(");");

            return script.ToString();
        }
        private string Build_PG_InsertMessageScript(ApplicationObject metaObject)
        {
            Dictionary<string, string> fields = GetDatabaseInterface(metaObject);

            int counter = 0;
            StringBuilder fieldNames = new StringBuilder();
            StringBuilder paramNames = new StringBuilder();
            foreach (KeyValuePair<string, string> field in fields)
            {
                counter++;

                paramNames.Append($"@{field.Key}");
                fieldNames.Append($"[{field.Value}]");

                if (counter != fields.Count)
                {
                    paramNames.Append($", ");
                    fieldNames.Append($", ");
                }
            }

            StringBuilder script = new StringBuilder();
            script.AppendLine($"INSERT [{metaObject.TableName}]");

            script.Append("(");
            script.Append(fieldNames.ToString());
            script.AppendLine(")");

            script.Append("VALUES (");
            script.Append(paramNames.ToString());
            script.Append(");");

            return script.ToString();
        }



        public void InsertMessage(DatabaseIncomingMessage message)
        {
            if (DatabaseProvider == DatabaseProvider.SQLServer)
            {
                MS_InsertMessage(message);
            }
            else
            {
                PG_InsertMessage(message);
            }
        }
        
        private void MS_ConfigureInsertCommand(DatabaseIncomingMessage message, SqlCommand command)
        {
            command.Parameters.Clear();

            command.Parameters.AddWithValue("Отправитель", message.Sender);
            command.Parameters.AddWithValue("ТипОперации", message.OperationType);
            command.Parameters.AddWithValue("ТипСообщения", message.MessageType);
            command.Parameters.AddWithValue("ТелоСообщения", message.MessageBody);
            command.Parameters.AddWithValue("ОписаниеОшибки", message.ErrorDescription);
            command.Parameters.AddWithValue("КоличествоОшибок", message.ErrorCount);
            if (_YearOffset > 0)
            {
                command.Parameters.AddWithValue("ДатаВремя", message.DateTimeStamp.AddYears(_YearOffset));
            }
            else
            {
                command.Parameters.AddWithValue("ДатаВремя", message.DateTimeStamp);
            }
        }
        private void MS_InsertMessage(DatabaseIncomingMessage message)
        {
            int recordsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = InsertMessageScript;
                command.CommandTimeout = 10; // seconds

                MS_ConfigureInsertCommand(message, command);

                connection.Open();
                recordsAffected = command.ExecuteNonQuery();
            }

            if (recordsAffected == 0)
            {
                throw new Exception("Failed to insert message into the database.");
            }
        }

        private void PG_InsertMessage(DatabaseIncomingMessage message)
        {
            // TODO
        }

        //public bool PostgreSQL_InsertMessage(DatabaseMessage message)
        //{
        //    int recordsAffected = 0;
        //    {
        //        NpgsqlConnection connection = new NpgsqlConnection(Settings.DatabaseSettings.ConnectionString);
        //        NpgsqlCommand command = connection.CreateCommand();
        //        command.CommandType = CommandType.Text;
        //        command.CommandText = PostgreSQL_InsertMessageScript();
        //        command.CommandTimeout = 60; // seconds

        //        command.Parameters.AddWithValue("p1", message.Code);
        //        command.Parameters.AddWithValue("p2", message.Uuid.ToByteArray());
        //        command.Parameters.AddWithValue("p3", message.DateTimeStamp);
        //        command.Parameters.AddWithValue("p4", message.Sender);
        //        command.Parameters.AddWithValue("p5", message.OperationType);
        //        command.Parameters.AddWithValue("p6", message.MessageType);
        //        command.Parameters.AddWithValue("p7", message.MessageBody);
        //        command.Parameters.AddWithValue("p8", message.ErrorCount);
        //        command.Parameters.AddWithValue("p9", message.ErrorDescription);

        //        try
        //        {
        //            connection.Open();
        //            recordsAffected = command.ExecuteNonQuery();
        //        }
        //        catch (Exception error)
        //        {
        //            // TODO: replace with using statement and handle exceptions outside the method !?
        //            FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error));
        //        }
        //        finally
        //        {
        //            DisposeDatabaseResources(connection, null, command);
        //        }
        //    }
        //    if (Settings.DebugMode)
        //    {
        //        throw new Exception("Failed to insert message to database. Records affected = " + recordsAffected.ToString());
        //    }
        //    return (recordsAffected != 0);
        //}
        //private string PostgreSQL_InsertMessageScript()
        //{
        //    DatabaseQueue queue = Settings.DatabaseSettings.DatabaseQueue;
        //    string tableName = queue.TableName;
        //    string field1 = queue.Fields.Where(f => f.Property == "МоментВремени").FirstOrDefault()?.Name;
        //    string field2 = queue.Fields.Where(f => f.Property == "Идентификатор").FirstOrDefault()?.Name;
        //    string field3 = queue.Fields.Where(f => f.Property == "ДатаВремя").FirstOrDefault()?.Name;
        //    string field4 = queue.Fields.Where(f => f.Property == "Отправитель").FirstOrDefault()?.Name;
        //    string field5 = queue.Fields.Where(f => f.Property == "ТипОперации").FirstOrDefault()?.Name;
        //    string field6 = queue.Fields.Where(f => f.Property == "ТипСообщения").FirstOrDefault()?.Name;
        //    string field7 = queue.Fields.Where(f => f.Property == "ТелоСообщения").FirstOrDefault()?.Name;
        //    string field8 = queue.Fields.Where(f => f.Property == "КоличествоОшибок").FirstOrDefault()?.Name;
        //    string field9 = queue.Fields.Where(f => f.Property == "ОписаниеОшибки").FirstOrDefault()?.Name;

        //    StringBuilder script = new StringBuilder();
        //    script.AppendLine($"INSERT INTO {tableName}");
        //    script.AppendLine($"({field1}, {field2}, {field3}, {field4}, {field5}, {field6}, {field7}, {field8}, {field9})");
        //    script.AppendLine("VALUES (@p1, @p2, @p3, CAST(@p4 AS mvarchar), CAST(@p5 AS mvarchar), ");
        //    script.AppendLine("CAST(@p6 AS mvarchar), CAST(@p7 AS mvarchar), @p8, CAST(@p9 AS mvarchar));");
        //    return script.ToString();
        //}
    }
}