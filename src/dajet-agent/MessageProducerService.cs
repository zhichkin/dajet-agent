using DaJet.Agent.Service;
using DaJet.Data.Messaging;
using DaJet.Logging;
using DaJet.Metadata;
using DaJet.Metadata.Model;
using DaJet.RabbitMQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ExchangePlanHelper = DaJet.Agent.Service.ExchangePlanHelper;
using OptionsFactory = Microsoft.Extensions.Options.Options;

namespace DaJet.Agent.Producer
{
    internal sealed class MessageProducerService : BackgroundService
    {
        private readonly IMetadataCache _metadataCache;

        private const string DELAY_MESSAGE_TEMPLATE = "Message producer service delay for {0} seconds.";
        private const string RETRY_MESSAGE_TEMPLATE = "Message producer service will retry in {0} seconds.";
        private AppSettings Options { get; set; }
        private MessageProducerSettings Settings { get; set; }
        private readonly IOptions<RmqProducerOptions> _options = OptionsFactory.Create(new RmqProducerOptions());
        public MessageProducerService(IOptions<AppSettings> options, IOptions<MessageProducerSettings> settings, IMetadataCache cache)
        {
            _metadataCache = cache;
            Options = options.Value;
            Settings = settings.Value;
            
            _options.Value.UseDeliveryTracking = Options.UseDeliveryTraking;

            if (Settings.UseVectorService)
            {
                _options.Value.UseVectorService = Settings.UseVectorService;
                _options.Value.VectorDatabase = Path.Combine(Options.AppCatalog, "producer-vector.db");
            }

            _options.Value.ErrorLogRetention = Settings.ErrorLogRetention;
            _options.Value.ErrorLogDatabase = Path.Combine(Options.AppCatalog, "producer-errors.db");
            _options.Value.MessagesPerTransaction = Settings.DatabaseSettings.MessagesPerTransaction;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Message producer service is started.");
            // StartAsync calls and awaits ExecuteAsync inside
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Message producer service is stopping ...");
            // Do shutdown cleanup here (see HostOptions.ShutdownTimeout)
            FileLogger.Log("Message producer service is stopped.");
            return base.StopAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            // Running the service in the background
            _ = Task.Run(async () => { await DoWork(cancellationToken); }, cancellationToken);
            // Return completed task to let other services to run
            return Task.CompletedTask;
        }
        private async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                int delay = Settings.DatabaseSettings.DatabaseQueryingPeriodicity;

                try
                {
                    TryDoWork(cancellationToken);
                }
                catch (Exception error)
                {
                    FileLogger.LogException(error);
                    delay = Settings.CriticalErrorDelay;
                }
                
                FileLogger.Log(string.Format(DELAY_MESSAGE_TEMPLATE, delay));

                await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);
            }
        }
        private void TryDoWork(CancellationToken cancellationToken)
        {
            string uri = Settings.MessageBrokerSettings.BuildUri();

            if (Options.ExchangePlans == null || Options.ExchangePlans.Count == 0)
            {
                Options.ExchangePlans = new List<string>() { "ПланОбмена.ПланОбменаДанными" };
            }

            GetMessagingSettingsWithRetry(out ApplicationObject queue, cancellationToken);

            using (IMessageConsumer consumer = GetMessageConsumer(in queue))
            {
                using (RmqMessageProducer producer = new RmqMessageProducer(uri, string.Empty))
                {
                    producer.Configure(_options);

                    if (Settings.MessageBrokerSettings.ExchangeRole == 0)
                    {
                        producer.Initialize(ExchangeRoles.Aggregator);
                    }
                    else
                    {
                        producer.Initialize(ExchangeRoles.Dispatcher);
                    }

                    int published = producer.Publish(consumer);

                    FileLogger.Log($"Published {published} messages.");
                }
            }
        }
        private IMessageConsumer GetMessageConsumer(in ApplicationObject queue)
        {
            if (Settings.DatabaseSettings.DatabaseProvider == DatabaseProvider.SQLServer)
            {
                return new MsMessageConsumer(Settings.DatabaseSettings.ConnectionString, in queue);
            }
            else
            {
                return new PgMessageConsumer(Settings.DatabaseSettings.ConnectionString, in queue);
            }
        }
        private void GetMessagingSettingsWithRetry(out ApplicationObject queue, CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    if (!_metadataCache.TryGet(out InfoBase infoBase))
                    {
                        throw new Exception("Failed to get metadata from cache.");
                    }

                    queue = infoBase.GetApplicationObjectByName(Settings.OutgoingQueueName);
                    
                    if (queue == null)
                    {
                        throw new Exception($"Объект метаданных \"{Settings.OutgoingQueueName}\" не найден.");
                    }

                    DbInterfaceValidator validator = new DbInterfaceValidator();
                    int version = validator.GetOutgoingInterfaceVersion(in queue);

                    if (version < 1)
                    {
                        throw new Exception("Не удалось определить версию контракта данных.");
                    }

                    ExchangePlanHelper settings = new ExchangePlanHelper(in infoBase,
                        Settings.DatabaseSettings.DatabaseProvider,
                        Settings.DatabaseSettings.ConnectionString);
                    settings.ConfigureSelectScripts(Options.ExchangePlans[0]);
                    _options.Value.ThisNode = settings.GetThisNode();

                    if (ConfigureOutgoingQueue(version, in queue)) { return; }
                }
                catch (Exception error)
                {
                    FileLogger.Log("Message producer service: failed to get messaging settings.");
                    FileLogger.LogException(error);
                }

                FileLogger.Log(string.Format(RETRY_MESSAGE_TEMPLATE, Settings.CriticalErrorDelay));

                Task.Delay(TimeSpan.FromSeconds(Settings.CriticalErrorDelay), cancellationToken).Wait(cancellationToken);
            }
        }
        private bool ConfigureOutgoingQueue(int version, in ApplicationObject queue)
        {
            DbQueueConfigurator configurator = new DbQueueConfigurator(
                version,
                Settings.DatabaseSettings.DatabaseProvider,
                Settings.DatabaseSettings.ConnectionString);
            
            configurator.ConfigureOutgoingMessageQueue(in queue, out List<string> errors);

            if (errors.Count > 0)
            {
                foreach (string error in errors)
                {
                    FileLogger.Log(error);
                }

                return false;
            }

            FileLogger.Log("Исходящая очередь настроена успешно.");

            return true;
        }
    }
}