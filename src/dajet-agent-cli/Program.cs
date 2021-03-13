using DaJet.Agent.Consumer;
using DaJet.Agent.Producer;
using DaJet.Metadata;
using DaJet.Metadata.Mappers;
using DaJet.Metadata.Model;
using RabbitMQ.Client;
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

        private const string EXCHANGE_NAMESPACE = "РИБ";
        private const string METAOBJECT_BASE_NAME = "Справочник";
        private const string CONSUMER_TABLE_QUEUE_NAME = "ВходящаяОчередьRabbitMQ";
        private const string PRODUCER_TABLE_QUEUE_NAME = "ИсходящаяОчередьRabbitMQ";
        private const string CONSUMER_SETTINGS_FILE_NAME = "consumer-settings.json";
        private const string PRODUCER_SETTINGS_FILE_NAME = "producer-settings.json";
        private const string SERVER_IS_NOT_DEFINED_ERROR = "Server address is not defined.";
        private const string DATABASE_IS_NOT_DEFINED_ERROR = "Database name is not defined.";
        private const string OUTPUT_CATALOG_IS_NOT_DEFINED = "Output catalog is not defined.";
        private const string CREATE_RABBITMQ_QUEUES_QUESTION = "Create RabbitMQ message queues for \"{0}\" publication ? [Y/N] ";
        private const string INPUT_HOST_TEXT = "RabbitMQ host (localhost): ";
        private const string INPUT_PORT_TEXT = "RabbitMQ port (5672): ";
        private const string INPUT_USER_TEXT = "RabbitMQ user (guest): ";
        private const string INPUT_PASS_TEXT = "RabbitMQ pass (guest): ";
        private const string LOADING_METADATA_NOTICE = "Loading 1C metadata ...";
        private const string METADATA_LOADED_NOTICE = "1C metadata loaded successfully.";
        private const string PUBLICATION_IS_NOT_FOUND_WARNING = "Publication \"{0}\" is not found.";
        private const string FAILED_TO_CREATE_QUEUE_ERROR = "Failed to create queue \"{0}\".";
        private const string QUEUE_CREATED_SUCCESSFULLY_NOTICE = "Queue \"{0}\" created successfully.";
        private const string PRESS_ANY_KEY_TO_EXIT_MESSAGE = "Press any key to exit.";
        private const string SETTINGS_FILES_ARE_GENERATED_SUCCESSFULLY = "Settings files are generated successfully.";
        private const string SETTINGS_FILES_CATALOG_PATH_NOTICE = "Settings files catalog path: {0}";

        #endregion

        public static int Main(string[] args)
        {
            //args = new string[] { "--ms", "ZHICHKIN", "--d", "test_node_1", "--o", "C:\\temp" };
            //args = new string[] { "--ms", "ZHICHKIN", "--d", "my_exchange", "--rmq", "Тестовый" };
            //args = new string[] { "--pg", "127.0.0.1", "--d", "test_node_2", "--u", "postgres", "--p", "postgres", "--rmq", "Тестовый" };

            RootCommand command = new RootCommand()
            {
                new Option<string>("--ms", "Microsoft SQL Server address or name"),
                new Option<string>("--pg", "PostgresSQL server address or name"),
                new Option<string>("--d", "Database name"),
                new Option<string>("--u", "User name (Windows authentication is used if not defined)"),
                new Option<string>("--p", "User password if SQL Server authentication is used"),
                new Option<string>("--rmq", "Publication name to use for RabbitMQ queues creation"),
                new Option<FileInfo>("--o", "Catalog path to save settings files")
            };
            command.Description = "DaJet Agent CLI (settings files generation and RabbitMQ queues creation tool)";
            command.Handler = CommandHandler.Create<string, string, string, string, string, string, FileInfo>(ExecuteCommand);
            return command.Invoke(args);
        }
        private static void ShowErrorMessage(string errorText)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorText);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void ExecuteCommand(string ms, string pg, string d, string u, string p, string rmq, FileInfo o)
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
                Console.WriteLine(OUTPUT_CATALOG_IS_NOT_DEFINED);
            }
            else
            {
                GenerateSettingsFiles(ms, pg, d, u, p, o);
                Console.WriteLine(SETTINGS_FILES_ARE_GENERATED_SUCCESSFULLY);
                Console.WriteLine(string.Format(SETTINGS_FILES_CATALOG_PATH_NOTICE, o.FullName));
            }

            if (!string.IsNullOrWhiteSpace(rmq))
            {
                try
                {
                    CreateRabbitMQQueues(ms, pg, d, u, p, rmq);
                }
                catch (Exception error)
                {
                    Console.WriteLine(Utilities.ExceptionHelper.GetErrorText(error));
                }
                Console.WriteLine();
                Console.WriteLine(PRESS_ANY_KEY_TO_EXIT_MESSAGE);
                Console.ReadKey(false);
            }
        }

        #region "Settings files generation"

        private static void GenerateSettingsFiles(string ms, string pg, string d, string u, string p, FileInfo o)
        {
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
                DatabaseProvider = MSSQL ? Utilities.DatabaseProviders.SQLServer : Utilities.DatabaseProviders.PostgreSQL,
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
                DatabaseProvider = MSSQL ? Utilities.DatabaseProviders.SQLServer : Utilities.DatabaseProviders.PostgreSQL,
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

        #endregion

        #region "RabbitMQ message queues creation"

        private static void CreateRabbitMQQueues(string ms, string pg, string d, string u, string p, string rmq)
        {
            Console.Write(string.Format(CREATE_RABBITMQ_QUEUES_QUESTION, rmq));
            string answer = Console.ReadLine();
            if (answer.ToLowerInvariant() != "y") return;

            Console.Write(INPUT_HOST_TEXT);
            string host = Console.ReadLine();

            Console.Write(INPUT_PORT_TEXT);
            string port = Console.ReadLine();

            Console.Write(INPUT_USER_TEXT);
            string user = Console.ReadLine();

            Console.Write(INPUT_PASS_TEXT);
            string pass = Console.ReadLine();

            MessageProducerSettings settings = CreateMessageBrokerSettings(host, port, user, pass);

            Metadata.DatabaseProviders provider;
            IMetadataFileReader fileReader = null;
            if (!string.IsNullOrWhiteSpace(ms))
            {
                provider = Metadata.DatabaseProviders.SQLServer;
                fileReader = new MetadataFileReader();
                fileReader.ConfigureConnectionString(ms, d, u, p);
            }
            else if (!string.IsNullOrWhiteSpace(pg))
            {
                provider = Metadata.DatabaseProviders.PostgreSQL;
                fileReader = new PostgresMetadataFileReader();
                fileReader.ConfigureConnectionString(pg, d, u, p);
            }
            else
            {
                provider = Metadata.DatabaseProviders.SQLServer;
            }

            Console.WriteLine(LOADING_METADATA_NOTICE);
            IMetadataReader metadata = new MetadataReader(fileReader);
            InfoBase infoBase = metadata.LoadInfoBase();
            Console.WriteLine(METADATA_LOADED_NOTICE);

            MetaObject metaObject = infoBase.Publications.Values
                .Where(p => p.Name == rmq).FirstOrDefault();
            if (metaObject == null)
            {
                Console.WriteLine(string.Format(PUBLICATION_IS_NOT_FOUND_WARNING, rmq));
                return;
            }

            if (!(metaObject is Publication publication))
            {
                Console.WriteLine(string.Format(PUBLICATION_IS_NOT_FOUND_WARNING, rmq));
                return;
            }

            PublicationDataMapper mapper = new PublicationDataMapper();
            mapper.UseConnectionString(fileReader.ConnectionString);
            mapper.UseDatabaseProvider(provider);
            mapper.SelectSubscribers(publication);

            using (IConnection messageBroker = CreateConnection(settings.MessageBrokerSettings))
            {
                string queueName;
                foreach (Subscriber subscriber in publication.Subscribers)
                {
                    if (subscriber.IsMarkedForDeletion) continue;
                    queueName = CreateQueueName(publication.Publisher.Code, subscriber.Code);
                    CreateExchangeAndQueue(messageBroker, queueName);
                    queueName = CreateQueueName(subscriber.Code, publication.Publisher.Code);
                    CreateExchangeAndQueue(messageBroker, queueName);
                }
            }   
        }
        private static string CreateQueueName(string producer, string consumer)
        {
            return $"{EXCHANGE_NAMESPACE}.{producer}.{consumer}";
        }
        private static IConnection CreateConnection(Producer.MessageBrokerSettings settings)
        {
            IConnectionFactory factory = new ConnectionFactory()
            {
                HostName = settings.HostName,
                UserName = settings.UserName,
                Password = settings.Password,
                Port = settings.PortNumber
            };
            return factory.CreateConnection();
        }
        private static MessageProducerSettings CreateMessageBrokerSettings(string host, string port, string user, string pass)
        {
            return new MessageProducerSettings()
            {
                MessageBrokerSettings = new Producer.MessageBrokerSettings()
                {
                    HostName = host,
                    UserName = user,
                    Password = pass,
                    PortNumber = int.Parse(port)
                }
            };
        }
        private static void CreateExchangeAndQueue(IConnection messageBroker, string queueName)
        {
            try
            {
                IModel channel = messageBroker.CreateModel();

                channel.ExchangeDeclare(queueName, ExchangeType.Direct, true, false, null);

                QueueDeclareOk queue = channel.QueueDeclare(queueName, true, false, false, null);
                if (queue == null)
                {
                    Console.WriteLine(string.Format(FAILED_TO_CREATE_QUEUE_ERROR, queueName));
                }
                else
                {
                    channel.QueueBind(queueName, queueName, string.Empty, null);
                    Console.WriteLine(string.Format(QUEUE_CREATED_SUCCESSFULLY_NOTICE, queueName));
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(string.Format(FAILED_TO_CREATE_QUEUE_ERROR, queueName));
                Console.WriteLine(Utilities.ExceptionHelper.GetErrorText(error));
            }
        }

        #endregion
    }
}