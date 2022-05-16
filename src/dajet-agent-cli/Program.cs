using DaJet.Agent.Consumer;
using DaJet.Agent.Producer;
using DaJet.Metadata;
using DaJet.Metadata.Model;
using System;
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

        private const string EXCHANGE_NAMESPACE = "РИБ";
        private const string PRODUCER_EXCHANGE_NAME = "АПО";
        private const string CONSUMER_TABLE_QUEUE_NAME = "ВходящаяОчередьRabbitMQ";
        private const string PRODUCER_TABLE_QUEUE_NAME = "ИсходящаяОчередьRabbitMQ";
        private const string CONSUMER_SETTINGS_FILE_NAME = "consumer-settings.json";
        private const string PRODUCER_SETTINGS_FILE_NAME = "producer-settings.json";
        private const string SERVER_IS_NOT_DEFINED_ERROR = "Server address is not defined.";
        private const string DATABASE_IS_NOT_DEFINED_ERROR = "Database name is not defined.";
        private const string OUTPUT_CATALOG_IS_NOT_DEFINED = "Output catalog is not defined.";
        private const string SETTINGS_FILES_ARE_GENERATED_SUCCESSFULLY = "Settings files are generated successfully.";
        private const string SETTINGS_FILES_CATALOG_PATH_NOTICE = "Settings files catalog path: {0}";

        #endregion

        public static int Main(string[] args)
        {
            //args = new string[] { "--help" };

            RootCommand command = new RootCommand()
            {
                new Option<string>("--ms", "Microsoft SQL Server address or name"),
                new Option<string>("--pg", "PostgresSQL server address or name"),
                new Option<string>("--d", "Database name"),
                new Option<string>("--u", "User name (Windows authentication is used if not defined)"),
                new Option<string>("--p", "User password if SQL Server authentication is used"),
                new Option<FileInfo>("--o", "Catalog path to save settings files")
            };
            command.Description = "DaJet Agent CLI 4.1.1 (settings files generation)";
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
                ShowErrorMessage(OUTPUT_CATALOG_IS_NOT_DEFINED); return;
            }
            
            GenerateSettingsFiles(ms, pg, d, u, p, o);
            Console.WriteLine(SETTINGS_FILES_ARE_GENERATED_SUCCESSFULLY);
            Console.WriteLine(string.Format(SETTINGS_FILES_CATALOG_PATH_NOTICE, o.FullName));
        }

        #region "Settings files generation"

        private static void GenerateSettingsFiles(string ms, string pg, string d, string u, string p, FileInfo o)
        {
            IMetadataService metadataService = new MetadataService();
            if (!string.IsNullOrWhiteSpace(ms))
            {
                metadataService.UseDatabaseProvider(DatabaseProvider.SQLServer);
                metadataService.ConfigureConnectionString(ms, d, u, p);
            }
            else
            {
                metadataService.UseDatabaseProvider(DatabaseProvider.PostgreSQL);
                metadataService.ConfigureConnectionString(pg, d, u, p);
            }

            //InfoBase infoBase = metadataService.LoadInfoBase();

            MessageConsumerSettings consumerSettings = CreateConsumerSettings(metadataService);
            MessageProducerSettings producerSettings = CreateProducerSettings(metadataService);

            string consumerPath = Path.Combine(o.FullName, CONSUMER_SETTINGS_FILE_NAME);
            string producerPath = Path.Combine(o.FullName, PRODUCER_SETTINGS_FILE_NAME);

            SaveConsumerSettings(consumerPath, consumerSettings);
            SaveProducerSettings(producerPath, producerSettings);
        }

        private static MessageConsumerSettings CreateConsumerSettings(IMetadataService metadataService)
        {
            MessageConsumerSettings settings = new MessageConsumerSettings();

            //ApplicationObject metaObject = infoBase.InformationRegisters.Values
            //    .Where(с => с.Name == CONSUMER_TABLE_QUEUE_NAME).FirstOrDefault();

            //if (metaObject == null) return settings;

            settings.DatabaseSettings = new Consumer.DatabaseSettings()
            {
                DatabaseProvider = metadataService.DatabaseProvider,
                ConnectionString = metadataService.ConnectionString
            };

            return settings;
        }
        private static void SaveConsumerSettings(string filePath, MessageConsumerSettings consumerSettings)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(consumerSettings, options);
            string json = Encoding.UTF8.GetString(bytes);
            using (StreamWriter stream = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                stream.Write(json);
            }
        }

        private static MessageProducerSettings CreateProducerSettings(IMetadataService metadataService)
        {
            MessageProducerSettings settings = new MessageProducerSettings();

            settings.MessageBrokerSettings.TopicExchange = string.Format("{0}.{1}", EXCHANGE_NAMESPACE, PRODUCER_EXCHANGE_NAME);

            //ApplicationObject metaObject = infoBase.InformationRegisters.Values
            //    .Where(с => с.Name == PRODUCER_TABLE_QUEUE_NAME).FirstOrDefault();

            //if (metaObject == null) return settings;

            settings.DatabaseSettings = new Producer.DatabaseSettings()
            {
                DatabaseProvider = metadataService.DatabaseProvider,
                ConnectionString = metadataService.ConnectionString,
                DatabaseQueryingPeriodicity = 5 // seconds
            };

            return settings;
        }
        private static void SaveProducerSettings(string filePath, MessageProducerSettings producerSettings)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(producerSettings, options);
            string json = Encoding.UTF8.GetString(bytes);
            using (StreamWriter stream = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                stream.Write(json);
            }
        }

        #endregion
    }
}