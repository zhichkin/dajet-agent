using DaJet.Agent.Service;
using DaJet.Data.Messaging;
using DaJet.Logging;
using DaJet.Metadata;
using DaJet.Metadata.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DaJet.Agent.Kafka.Producer
{
    internal sealed class KafkaProducerService : BackgroundService
    {
        private const string DELAY_MESSAGE_TEMPLATE = "Kafka producer service delay for {0} seconds.";
        private const string RETRY_MESSAGE_TEMPLATE = "Kafka producer service will retry in {0} seconds.";

        private readonly IMetadataCache _metadataCache;
        private readonly KafkaProducerSettings _options;
        public KafkaProducerService(IOptions<AppSettings> options, IMetadataCache cache)
        {
            _metadataCache = cache;
            _options = options.Value.Kafka.Producer;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Kafka producer service is started.");
            // StartAsync calls and awaits ExecuteAsync inside
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Kafka producer service is stopping ...");
            // Do shutdown cleanup here (see HostOptions.ShutdownTimeout)
            FileLogger.Log("Kafka producer service is stopped.");
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
                int delay;

                try
                {
                    TryDoWork(cancellationToken);
                    delay = _options.IdleDelay;
                    FileLogger.Log(string.Format(DELAY_MESSAGE_TEMPLATE, delay));
                }
                catch (Exception error)
                {
                    delay = _options.ErrorDelay;
                    FileLogger.LogException(error);
                    FileLogger.Log(string.Format(RETRY_MESSAGE_TEMPLATE, delay));
                }
                await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);
            }
        }
        private void TryDoWork(CancellationToken cancellationToken)
        {
            GetMessagingSettingsWithRetry(out ApplicationObject queue, cancellationToken);

            using (IMessageConsumer consumer = GetMessageConsumer(in queue))
            {
                using (KafkaMessageProducer producer = new KafkaMessageProducer(_options))
                {
                    int published = producer.Publish(consumer);
                    FileLogger.Log($"[Kafka] Published {published} messages.");
                }
            }
        }
        private DatabaseProvider GetDatabaseProviderFromConnectionString()
        {
            return _options.ConnectionString.StartsWith("Host")
                ? DatabaseProvider.PostgreSQL
                : DatabaseProvider.SQLServer;
        }
        private IMessageConsumer GetMessageConsumer(in ApplicationObject queue)
        {
            DatabaseProvider provider = GetDatabaseProviderFromConnectionString();

            if (provider == DatabaseProvider.SQLServer)
            {
                return new MsMessageConsumer(_options.ConnectionString, in queue);
            }
            else
            {
                return new PgMessageConsumer(_options.ConnectionString, in queue);
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
                        throw new Exception("[Kafka] Failed to get metadata from cache.");
                    }

                    queue = infoBase.GetApplicationObjectByName(_options.OutgoingQueueName);

                    if (queue == null)
                    {
                        throw new Exception($"[Kafka] Объект метаданных \"{_options.OutgoingQueueName}\" не найден.");
                    }

                    DbInterfaceValidator validator = new DbInterfaceValidator();
                    int version = validator.GetOutgoingInterfaceVersion(in queue);

                    if (version < 1)
                    {
                        throw new Exception("[Kafka] Не удалось определить версию контракта данных.");
                    }

                    if (ConfigureOutgoingQueue(version, in queue)) { return; }
                }
                catch (Exception error)
                {
                    FileLogger.Log("Kafka producer service: failed to get messaging settings.");
                    FileLogger.LogException(error);
                }

                FileLogger.Log(string.Format(RETRY_MESSAGE_TEMPLATE, _options.ErrorDelay));

                Task.Delay(TimeSpan.FromSeconds(_options.ErrorDelay), cancellationToken).Wait(cancellationToken);
            }
        }
        private bool ConfigureOutgoingQueue(int version, in ApplicationObject queue)
        {
            DatabaseProvider provider = GetDatabaseProviderFromConnectionString();

            DbQueueConfigurator configurator = new DbQueueConfigurator(version, provider, _options.ConnectionString);

            configurator.ConfigureOutgoingMessageQueue(in queue, out List<string> errors);

            if (errors.Count > 0)
            {
                foreach (string error in errors)
                {
                    FileLogger.Log(error);
                }

                return false;
            }

            FileLogger.Log("[Kafka] Исходящая очередь настроена успешно.");

            return true;
        }
    }
}