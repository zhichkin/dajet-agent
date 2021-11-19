using DaJet.Agent.Consumer;
using DaJet.Agent.Producer;
using DaJet.Metadata;
using DaJet.Metadata.Mappers;
using DaJet.Metadata.Model;
using RabbitMQ.Client;
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
        private const string METAOBJECT_BASE_NAME = "РегистрСведений";
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
            RootCommand command = new RootCommand()
            {
                new Option<string>("--ms", "Microsoft SQL Server address or name"),
                new Option<string>("--pg", "PostgresSQL server address or name"),
                new Option<string>("--d", "Database name"),
                new Option<string>("--u", "User name (Windows authentication is used if not defined)"),
                new Option<string>("--p", "User password if SQL Server authentication is used"),
                //new Option<string>("--rmq", "Publication name to use for RabbitMQ queues creation"),
                new Option<FileInfo>("--o", "Catalog path to save settings files")
            };
            command.Description = "DaJet Agent CLI (settings files generation)";
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

            //if (!string.IsNullOrWhiteSpace(rmq))
            //{
            //    try
            //    {
            //        CreateRabbitMQQueues(ms, pg, d, u, p, rmq);
            //    }
            //    catch (Exception error)
            //    {
            //        Console.WriteLine(ExceptionHelper.GetErrorText(error));
            //    }
            //    Console.WriteLine();
            //    Console.WriteLine(PRESS_ANY_KEY_TO_EXIT_MESSAGE);
            //    Console.ReadKey(false);
            //}
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

            InfoBase infoBase = metadataService.LoadInfoBase();

            MessageConsumerSettings consumerSettings = CreateConsumerSettings(infoBase, metadataService);
            MessageProducerSettings producerSettings = CreateProducerSettings(infoBase, metadataService);

            string consumerPath = Path.Combine(o.FullName, CONSUMER_SETTINGS_FILE_NAME);
            string producerPath = Path.Combine(o.FullName, PRODUCER_SETTINGS_FILE_NAME);

            SaveConsumerSettings(consumerPath, consumerSettings);
            SaveProducerSettings(producerPath, producerSettings);
        }

        private static MessageConsumerSettings CreateConsumerSettings(InfoBase infoBase, IMetadataService metadataService)
        {
            MessageConsumerSettings settings = new MessageConsumerSettings();

            ApplicationObject metaObject = infoBase.InformationRegisters.Values
                .Where(с => с.Name == CONSUMER_TABLE_QUEUE_NAME).FirstOrDefault();

            if (metaObject == null) return settings;

            //metadataService.EnrichFromDatabase(metaObject);

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

        private static MessageProducerSettings CreateProducerSettings(InfoBase infoBase, IMetadataService metadataService)
        {
            MessageProducerSettings settings = new MessageProducerSettings();

            settings.MessageBrokerSettings.TopicExchange = string.Format("{0}.{1}", EXCHANGE_NAMESPACE, PRODUCER_EXCHANGE_NAME);

            ApplicationObject metaObject = infoBase.InformationRegisters.Values
                .Where(с => с.Name == PRODUCER_TABLE_QUEUE_NAME).FirstOrDefault();

            if (metaObject == null) return settings;

            //metadataService.EnrichFromDatabase(metaObject);

            settings.DatabaseSettings = new Producer.DatabaseSettings()
            {
                DatabaseProvider = metadataService.DatabaseProvider,
                ConnectionString = metadataService.ConnectionString
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

            Console.WriteLine(LOADING_METADATA_NOTICE);
            InfoBase infoBase = metadataService.LoadInfoBase();
            Console.WriteLine(METADATA_LOADED_NOTICE);

            MetadataObject metaObject = infoBase.Publications.Values.Where(p => p.Name == rmq).FirstOrDefault();
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
            mapper.UseDatabaseProvider(metadataService.DatabaseProvider);
            mapper.UseConnectionString(metadataService.ConnectionString);
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
                Console.WriteLine(ExceptionHelper.GetErrorText(error));
            }
        }

        #endregion
    }
}