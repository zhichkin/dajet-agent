using DaJet.Agent.Producer;
using DaJet.Logging;
using DaJet.Metadata;
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
        private readonly MessageBrokerSettings _settings;
        private const string DELIVERY_TRACKING_QUEUE_NAME = "dajet-agent-monitor";
        private const string ERROR_MESSAGE_TEMPLATE = "[Delivery tracking] {0}";
        public DeliveryTrackingService(IOptions<MessageProducerSettings> settings)
        {
            _settings = new MessageBrokerSettings()
            {
                HostName = settings.Value.MessageBrokerSettings.HostName,
                PortNumber = settings.Value.MessageBrokerSettings.PortNumber,
                VirtualHost = settings.Value.MessageBrokerSettings.VirtualHost,
                UserName = settings.Value.MessageBrokerSettings.UserName,
                Password = settings.Value.MessageBrokerSettings.Password
            };
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
            int published = 0;
            string uri = _settings.BuildUri();
            using (EventTracker tracker = new EventTracker())
            {
                //published = tracker.Publish(uri, DELIVERY_TRACKING_QUEUE_NAME, cancellationToken);
            }
            FileLogger.Log($"[Delivery tracking] Published {published} events.");
        }
    }
}