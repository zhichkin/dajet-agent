using DaJet.Agent.Service;
using DaJet.Logging;
using DaJet.Metadata.Model;
using DaJet.RabbitMQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OptionsFactory = Microsoft.Extensions.Options.Options;
using ExchangePlanHelper = DaJet.Agent.Service.ExchangePlanHelper;

namespace DaJet.Agent.Consumer
{
    internal sealed class MessageConsumerService : BackgroundService
    {
        private readonly IMetadataCache _metadataCache;

        private const string INCOMING_QUEUE_NAME = "РегистрСведений.ВходящаяОчередьRabbitMQ";
        private const string DELAY_MESSAGE_TEMPLATE = "Message consumer service delay for {0} seconds.";
        private const string RETRY_MESSAGE_TEMPLATE = "Message consumer service will retry in {0} seconds.";
        private AppSettings Options { get; set; }
        private MessageConsumerSettings Settings { get; set; }
        public MessageConsumerService(IOptions<AppSettings> options, IOptions<MessageConsumerSettings> settings, IMetadataCache cache)
        {
            _metadataCache = cache;
            Options = options.Value;
            Settings = settings.Value;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Message consumer service is started.");
            // StartAsync calls and awaits ExecuteAsync inside
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Message consumer service is stopping ...");
            // Do shutdown cleanup here (see HostOptions.ShutdownTimeout)
            FileLogger.Log("Message consumer service is stopped.");
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
                    FileLogger.LogException(error);
                }
                
                FileLogger.Log(string.Format(DELAY_MESSAGE_TEMPLATE, Settings.CriticalErrorDelay));

                await Task.Delay(TimeSpan.FromSeconds(Settings.CriticalErrorDelay), cancellationToken);
            }
        }
        private void TryDoWork(CancellationToken cancellationToken)
        {
            string uri = Settings.MessageBrokerSettings.BuildUri();

            if (Options.ExchangePlans == null || Options.ExchangePlans.Count == 0)
            {
                Options.ExchangePlans = new List<string>() { "ПланОбмена.ПланОбменаДанными" };
            }

            GetMessagingSettingsWithRetry(out List<string> queues, cancellationToken);

            IOptions<RmqConsumerOptions> options = OptionsFactory.Create(
                new RmqConsumerOptions()
                {
                    Queues = queues, //TODO: update from 1C exchange plans
                    Heartbeat = Options.RefreshTimeout
                });

            using (RmqMessageConsumer consumer = new RmqMessageConsumer(uri, in queues))
            {
                consumer.Configure(options);

                consumer.Initialize(
                    Settings.DatabaseSettings.DatabaseProvider,
                    Settings.DatabaseSettings.ConnectionString,
                    INCOMING_QUEUE_NAME);

                consumer.Consume(cancellationToken, FileLogger.Log);
            }
        }
        private void GetMessagingSettingsWithRetry(out List<string> queues, CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    if (!_metadataCache.TryGet(out InfoBase infoBase))
                    {
                        throw new Exception("Failed to get metadata from cache.");
                    }

                    ExchangePlanHelper settings = new ExchangePlanHelper(
                        in infoBase,
                        Settings.DatabaseSettings.DatabaseProvider,
                        Settings.DatabaseSettings.ConnectionString);

                    queues = settings.GetIncomingQueueNames(Options.ExchangePlans);

                    return;
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
    }
}