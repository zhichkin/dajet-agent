using DaJet.Agent.Consumer;
using DaJet.Agent.Producer;
using DaJet.Metadata;
using DaJet.Metadata.Model;
using DaJet.Utilities;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace DaJet.Agent.CLI
{
    public static class Program
    {
        #region "Constants"

        private const string METAOBJECT_BASE_NAME = "Справочник";
        private const string CONSUMER_TABLE_QUEUE_NAME = "ВходящаяОчередьRabbitMQ";
        private const string PRODUCER_TABLE_QUEUE_NAME = "ИсходящаяОчередьRabbitMQ";
        private const string CONSUMER_SETTINGS_FILE_NAME = "consumer-settings.json";
        private const string PRODUCER_SETTINGS_FILE_NAME = "producer-settings.json";
        private const string SERVER_IS_NOT_DEFINED_ERROR = "Server address is not defined.";
        private const string DATABASE_IS_NOT_DEFINED_ERROR = "Database name is not defined.";
        private const string OUTPUT_CATALOG_IS_NOT_DEFINED_ERROR = "Output catalog is not defined.";

        #endregion

        public static int Main(string[] args)
        {
            //args = new string[] { "--ms", "ZHICHKIN", "--d", "test_node_1", "--o", "C:\\temp" };

            RootCommand command = new RootCommand()
            {
                new Option<string>("--ms", "Microsoft SQL Server address or name"),
                new Option<string>("--pg", "PostgresSQL server address or name"),
                new Option<string>("--d", "Database name"),
                new Option<string>("--u", "User name (Windows authentication is used if not defined)"),
                new Option<string>("--p", "User password if SQL Server authentication is used"),
                new Option<FileInfo>("--o", "Catalog path to save settings files")
            };
            command.Description = "DaJet (metadata reader utility)";
            command.Handler = CommandHandler.Create<string, string, string, string, string, FileInfo>(ExecuteCommand);
            return command.Invoke(args);
        }
        private static void ShowErrorMessage(string errorText)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorText);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void ExecuteCommand(string ms, string pg, string d, string u, string p, FileInfo o)
        {
            if (string.IsNullOrWhiteSpace(ms) && string.IsNullOrWhiteSpace(pg))
            {
                ShowErrorMessage(SERVER_IS_NOT_DEFINED_ERROR); return;
            }
            if (string.IsNullOrWhiteSpace(d))
            {
                ShowErrorMessage(DATABASE_IS_NOT_DEFINED_ERROR); return;
            }
            if (o == null)
            {
                ShowErrorMessage(OUTPUT_CATALOG_IS_NOT_DEFINED_ERROR); return;
            }

            IMetadataFileReader fileReader = null;
            if (!string.IsNullOrWhiteSpace(ms))
            {
                fileReader = new MetadataFileReader();
                fileReader.ConfigureConnectionString(ms, d, u, p);
            }
            else if (!string.IsNullOrWhiteSpace(pg))
            {
                fileReader = new PostgresMetadataFileReader();
                fileReader.ConfigureConnectionString(pg, d, u, p);
            }

            IMetadataReader metadata = new MetadataReader(fileReader);
            InfoBase infoBase = metadata.LoadInfoBase();

            MessageConsumerSettings consumerSettings = CreateConsumerSettings(infoBase, fileReader);
            MessageProducerSettings producerSettings = CreateProducerSettings(infoBase, fileReader);

            string consumerPath = Path.Combine(o.FullName, CONSUMER_SETTINGS_FILE_NAME);
            string producerPath = Path.Combine(o.FullName, PRODUCER_SETTINGS_FILE_NAME);

            SaveConsumerSettings(consumerPath, consumerSettings);
            SaveProducerSettings(producerPath, producerSettings);
        }

        private static MessageConsumerSettings CreateConsumerSettings(InfoBase infoBase, IMetadataFileReader fileReader)
        {
            MessageConsumerSettings settings = new MessageConsumerSettings();

            MetaObject metaObject = infoBase.Catalogs.Values.Where(с => с.Name == CONSUMER_TABLE_QUEUE_NAME).FirstOrDefault();
            if (metaObject == null) return settings;

            bool MSSQL = (fileReader is MetadataFileReader);
            string tableName = string.Empty;
            ISqlMetadataReader sqlReader = null;
            if (MSSQL)
            {
                tableName = metaObject.TableName;
                sqlReader = new SqlMetadataReader();
            }
            else
            {
                tableName = metaObject.TableName.ToLowerInvariant();
                sqlReader = new PostgresMetadataReader();
            }
            sqlReader.UseConnectionString(fileReader.ConnectionString);
            List<SqlFieldInfo> sqlFields = sqlReader.GetSqlFieldsOrderedByName(tableName);
            if (sqlFields.Count == 0)
            {
                return settings;
            }

            MetadataCompareAndMergeService merger = new MetadataCompareAndMergeService();
            merger.MergeProperties(metaObject, sqlFields);

            settings.DatabaseSettings = new Consumer.DatabaseSettings()
            {
                DatabaseProvider = MSSQL ? DatabaseProviders.SQLServer : DatabaseProviders.PostgreSQL,
                ConnectionString = fileReader.ConnectionString,
                DatabaseQueue = new Consumer.DatabaseQueue()
                {
                    TableName = tableName,
                    ObjectName = string.Format("{0}.{1}", METAOBJECT_BASE_NAME, CONSUMER_TABLE_QUEUE_NAME)
                }
            };
            foreach (MetaProperty property in metaObject.Properties)
            {
                foreach (MetaField field in property.Fields)
                {
                    settings.DatabaseSettings.DatabaseQueue.Fields.Add(new Consumer.TableField()
                    {
                        Name = MSSQL ? field.Name : field.Name.ToLowerInvariant(),
                        Property = property.Name
                    });
                }
            }

            return settings;
        }
        private static void SaveConsumerSettings(string filePath, MessageConsumerSettings consumerSettings)
        {
            JavaScriptEncoder encoder = JavaScriptEncoder.Create(new UnicodeRange(0, 0xFFFF));
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = encoder
            };
            byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(consumerSettings, options);
            string json = Encoding.UTF8.GetString(bytes);
            using (StreamWriter stream = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                stream.Write(json);
            }
        }

        private static MessageProducerSettings CreateProducerSettings(InfoBase infoBase, IMetadataFileReader fileReader)
        {
            MessageProducerSettings settings = new MessageProducerSettings();

            MetaObject metaObject = infoBase.Catalogs.Values.Where(с => с.Name == PRODUCER_TABLE_QUEUE_NAME).FirstOrDefault();
            if (metaObject == null) return settings;
            
            bool MSSQL = (fileReader is MetadataFileReader);
            string tableName = string.Empty;
            ISqlMetadataReader sqlReader = null;
            if (MSSQL)
            {
                tableName = metaObject.TableName;
                sqlReader = new SqlMetadataReader();
            }
            else
            {
                tableName = metaObject.TableName.ToLowerInvariant();
                sqlReader = new PostgresMetadataReader();
            }
            sqlReader.UseConnectionString(fileReader.ConnectionString);
            List<SqlFieldInfo> sqlFields = sqlReader.GetSqlFieldsOrderedByName(tableName);
            if (sqlFields.Count == 0)
            {
                return settings;
            }

            MetadataCompareAndMergeService merger = new MetadataCompareAndMergeService();
            merger.MergeProperties(metaObject, sqlFields);

            settings.DatabaseSettings = new Producer.DatabaseSettings()
            {
                DatabaseProvider = MSSQL ? DatabaseProviders.SQLServer : DatabaseProviders.PostgreSQL,
                ConnectionString = fileReader.ConnectionString,
                DatabaseQueue = new Producer.DatabaseQueue()
                {
                    TableName = tableName,
                    ObjectName = string.Format("{0}.{1}", METAOBJECT_BASE_NAME, PRODUCER_TABLE_QUEUE_NAME)
                }
            };
            foreach (MetaProperty property in metaObject.Properties)
            {
                foreach (MetaField field in property.Fields)
                {
                    settings.DatabaseSettings.DatabaseQueue.Fields.Add(new Producer.TableField()
                    {
                        Name = MSSQL ? field.Name : field.Name.ToLowerInvariant(),
                        Property = property.Name
                    });
                }
            }

            return settings;
        }
        private static void SaveProducerSettings(string filePath, MessageProducerSettings producerSettings)
        {
            JavaScriptEncoder encoder = JavaScriptEncoder.Create(new UnicodeRange(0, 0xFFFF));
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = encoder
            };
            byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(producerSettings, options);
            string json = Encoding.UTF8.GetString(bytes);
            using (StreamWriter stream = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                stream.Write(json);
            }
        }
    }
}