using DaJet.Data.Mapping;
using DaJet.Data.Messaging;
using DaJet.Json;
using DaJet.Logging;
using DaJet.Metadata;
using DaJet.Metadata.Model;
using DaJet.RabbitMQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DaJet.Agent.Producer
{
    internal sealed class MessageProducerService : BackgroundService
    {
        private const string OUTGOING_QUEUE_NAME = "РегистрСведений.ИсходящаяОчередьRabbitMQ";
        private const string DELAY_MESSAGE_TEMPLATE = "Message producer service delay for {0} seconds.";
        private const string RETRY_MESSAGE_TEMPLATE = "Message producer service will retry in {0} seconds.";
        private MessageProducerSettings Settings { get; set; }
        public MessageProducerService(IOptions<MessageProducerSettings> options)
        {
            Settings = options.Value;
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

            GetMessagingSettingsWithRetry(out InfoBase infoBase, out ApplicationObject queue, cancellationToken);

            EntityDataMapperProvider provider = new EntityDataMapperProvider(
                infoBase,
                Settings.DatabaseSettings.DatabaseProvider,
                Settings.DatabaseSettings.ConnectionString);

            EntityJsonSerializer serializer = new EntityJsonSerializer(provider);

            using (IMessageConsumer consumer = new MsMessageConsumer(Settings.DatabaseSettings.ConnectionString, in queue))
            {
                using (RmqMessageProducer producer = new RmqMessageProducer(uri, string.Empty))
                {
                    if (Settings.MessageBrokerSettings.ExchangeRole == 0)
                    {
                        producer.Initialize(ExchangeRoles.Aggregator);
                    }
                    else
                    {
                        producer.Initialize(ExchangeRoles.Dispatcher);
                    }

                    int published = producer.Publish(consumer, serializer);

                    FileLogger.Log($"Published {published} messages.");
                }
            }
        }
        private void GetMessagingSettingsWithRetry(out InfoBase infoBase, out ApplicationObject queue, CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    if (!new MetadataService()
                        .UseDatabaseProvider(Settings.DatabaseSettings.DatabaseProvider)
                        .UseConnectionString(Settings.DatabaseSettings.ConnectionString)
                        .TryOpenInfoBase(out infoBase, out string error))
                    {
                        throw new Exception(error);
                    }

                    queue = infoBase.GetApplicationObjectByName(OUTGOING_QUEUE_NAME);
                    
                    if (queue == null)
                    {
                        throw new Exception($"Объект метаданных \"{OUTGOING_QUEUE_NAME}\" не найден.");
                    }

                    DbInterfaceValidator validator = new DbInterfaceValidator();
                    int version = validator.GetOutgoingInterfaceVersion(in queue);

                    if (version < 1)
                    {
                        throw new Exception("Не удалось определить версию контракта данных.");
                    }

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