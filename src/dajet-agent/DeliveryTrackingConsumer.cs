using DaJet.Agent.Service;
using DaJet.Logging;
using DaJet.Metadata;
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
                await Task.Delay(TimeSpan.FromSeconds(300), cancellationToken);
            }
        }
        private void TryDoWork(CancellationToken cancellationToken)
        {
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
    }
}