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
using System.Threading;
using System.Threading.Tasks;
using OptionsFactory = Microsoft.Extensions.Options.Options;

namespace DaJet.Agent.Producer
{
    internal sealed class DeliveryTrackingConsumer : BackgroundService
    {
        private const string DELIVERY_TRACKING_QUEUE_NAME = "dajet-agent-monitor";
        private const string SERVICE_DELAY_MESSAGE_TEMPLATE = "[Delivery tracking] Service delay for {0} seconds.";
        private const string SERVICE_RETRY_MESSAGE_TEMPLATE = "[Delivery tracking] Service will retry in {0} seconds.";
        private AppSettings Options { get; set; }
        public DeliveryTrackingConsumer(IOptions<AppSettings> options)
        {
            Options = options.Value;

            if (Options.ConnectionString.StartsWith("Host"))
            {
                Options.DatabaseProvider = DatabaseProvider.PostgreSQL;
            }
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Delivery tracking consumer service is started.");
            // StartAsync calls and awaits ExecuteAsync inside
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Delivery tracking consumer service is stopping ...");
            // Do shutdown cleanup here (see HostOptions.ShutdownTimeout)
            FileLogger.Log("Delivery tracking consumer service is stopped.");
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
                try
                {
                    TryDoWork(cancellationToken);
                }
                catch (Exception error)
                {
                    FileLogger.Log($"[Delivery tracking] ERROR: {ExceptionHelper.GetErrorText(error)}");
                }
                FileLogger.Log(string.Format(SERVICE_DELAY_MESSAGE_TEMPLATE, Options.RefreshTimeout));
                await Task.Delay(TimeSpan.FromSeconds(Options.RefreshTimeout), cancellationToken);
            }
        }
        private void TryDoWork(CancellationToken cancellationToken)
        {
            ConfigureIncomingQueueWithRetry(cancellationToken);

            using (RmqMessageConsumer consumer = new RmqMessageConsumer(Options.MessageBrokerUri))
            {
                consumer.Configure(OptionsFactory.Create(new RmqConsumerOptions()
                {
                    Queues = new List<string>() { DELIVERY_TRACKING_QUEUE_NAME }
                }));

                consumer.Initialize(
                    Options.DatabaseProvider,
                    Options.ConnectionString,
                    Options.MonitorQueueName);

                consumer.Consume(cancellationToken, FileLogger.Log);
            }
        }
        private void ConfigureIncomingQueueWithRetry(CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    if (!new MetadataService()
                        .UseDatabaseProvider(Options.DatabaseProvider)
                        .UseConnectionString(Options.ConnectionString)
                        .TryOpenInfoBase(out InfoBase infoBase, out string error))
                    {
                        throw new Exception($"[Delivery tracking] Failed to get metadata: {error}");
                    }

                    ApplicationObject queue = infoBase.GetApplicationObjectByName(Options.MonitorQueueName);

                    if (queue == null)
                    {
                        throw new Exception($"[Delivery tracking] Объект метаданных \"{Options.MonitorQueueName}\" не найден.");
                    }

                    DbInterfaceValidator validator = new DbInterfaceValidator();
                    int version = validator.GetIncomingInterfaceVersion(in queue);

                    if (version < 1)
                    {
                        throw new Exception("[Delivery tracking] Не удалось определить версию контракта данных.");
                    }

                    if (ConfigureIncomingQueue(version, in queue)) { return; }
                }
                catch (Exception error)
                {
                    FileLogger.Log("[Delivery tracking] Failed to get messaging settings.");
                    FileLogger.LogException(error);
                }
                FileLogger.Log(string.Format(SERVICE_RETRY_MESSAGE_TEMPLATE, Options.RefreshTimeout));
                Task.Delay(TimeSpan.FromSeconds(Options.RefreshTimeout), cancellationToken).Wait(cancellationToken);
            }
        }
        private bool ConfigureIncomingQueue(int version, in ApplicationObject queue)
        {
            DbQueueConfigurator configurator = new DbQueueConfigurator(
                version,
                Options.DatabaseProvider,
                Options.ConnectionString);

            configurator.ConfigureIncomingMessageQueue(in queue, out List<string> errors);

            if (errors.Count > 0)
            {
                foreach (string error in errors)
                {
                    FileLogger.Log($"[Delivery tracking] ERROR: {error}");
                }

                return false;
            }

            FileLogger.Log("[Delivery tracking] Входящая очередь настроена успешно.");

            return true;
        }
    }
}