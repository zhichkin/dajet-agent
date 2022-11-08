using DaJet.Logging;
using DaJet.Metadata;
using DaJet.Metadata.Model;
using DaJet.RabbitMQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DaJet.Agent.Service
{
    internal sealed class DeliveryTrackingService : BackgroundService
    {
        private readonly AppSettings _settings;
        private readonly DaJetAgentOptions _options;
        private readonly IMetadataCache _metadataCache;
        private const string DELIVERY_TRACKING_QUEUE_NAME = "dajet-agent-monitor";
        public DeliveryTrackingService(IOptions<AppSettings> settings, IOptions<DaJetAgentOptions> options, IMetadataCache cache)
        {
            _options = options.Value;
            _settings = settings.Value;
            _metadataCache = cache;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Delivery tracking service is started.");
            // StartAsync calls and awaits ExecuteAsync inside
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Delivery tracking service is stopping ...");
            // Do shutdown cleanup here (see HostOptions.ShutdownTimeout)
            FileLogger.Log("Delivery tracking service is stopped.");
            return base.StopAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            // Block execution until completed
            ConfigureDatabaseWithRetry(cancellationToken);
            ConfigureThisNodeCodeWithRetry(cancellationToken);
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
                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            }
        }
        private void TryDoWork(CancellationToken cancellationToken)
        {
            int consumed;
            int published = 0;
            
            string uri = _options.GetRabbitMQUri();

            IOptions<RmqProducerOptions> options = Options.Create(new RmqProducerOptions()
            {
                ThisNode = _options.ThisNode,
                Provider = _options.DatabaseProvider,
                ConnectionString = _options.ConnectionString,
                UseDeliveryTracking = true
            });

            do
            {
                using (RmqMessageProducer producer = new RmqMessageProducer(uri, DELIVERY_TRACKING_QUEUE_NAME))
                {
                    producer.Configure(options);
                    producer.Initialize();
                    consumed = producer.PublishDeliveryTrackingEvents(cancellationToken);
                    published += consumed;
                }
            }
            while (consumed > 0 && !cancellationToken.IsCancellationRequested);

            FileLogger.Log($"[Delivery tracking] Published {published} events.");
        }
        private DeliveryTracker CreateDeliveryTracker()
        {
            if (_options.DatabaseProvider == DatabaseProvider.SQLServer)
            {
                return new MsDeliveryTracker(_options.ConnectionString);
            }
            return new PgDeliveryTracker(_options.ConnectionString);
        }
        private void ConfigureDatabaseWithRetry(CancellationToken cancellationToken)
        {
            DeliveryTracker tracker = CreateDeliveryTracker();
            do
            {
                try
                {
                    tracker.ConfigureDatabase();
                    FileLogger.Log($"[Delivery tracking] database configured successfully.");
                    return;
                }
                catch (Exception error)
                {
                    FileLogger.Log($"[Delivery tracking] failed to configure database: {ExceptionHelper.GetErrorText(error)}");
                }
                Task.Delay(TimeSpan.FromSeconds(10)).Wait(cancellationToken);
            }
            while (!cancellationToken.IsCancellationRequested);
        }
        private void ConfigureThisNodeCodeWithRetry(CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    if (!_metadataCache.TryGet(out InfoBase infoBase))
                    {
                        throw new Exception("Failed to get metadata from cache.");
                    }

                    ExchangePlanHelper publication = new ExchangePlanHelper(
                        in infoBase,
                        _options.DatabaseProvider,
                        _options.ConnectionString);

                    publication.ConfigureSelectScripts(_settings.ExchangePlans[0]);
                    
                    _options.ThisNode = publication.GetThisNode();
                    _settings.ThisNode = _options.ThisNode;

                    return;
                }
                catch (Exception error)
                {
                    FileLogger.Log("Failed to get exchange plan settings.");
                    FileLogger.LogException(error);
                }
                Task.Delay(TimeSpan.FromSeconds(10)).Wait(cancellationToken);
            }
        }
    }
}