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

namespace DaJet.Agent.Consumer
{
    internal sealed class MessageConsumerService : BackgroundService
    {
        private const string INCOMING_QUEUE_NAME = "РегистрСведений.ВходящаяОчередьRabbitMQ";
        private const string DELAY_MESSAGE_TEMPLATE = "Message consumer service delay for {0} seconds.";
        private const string RETRY_MESSAGE_TEMPLATE = "Message consumer service will retry in {0} seconds.";
        private MessageConsumerSettings Settings { get; set; }
        public MessageConsumerService(IOptions<MessageConsumerSettings> options)
        {
            Settings = options.Value;
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

            GetMessagingSettingsWithRetry(out List<string> queues, cancellationToken);

            using (RmqMessageConsumer consumer = new RmqMessageConsumer(uri, in queues))
            {
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
                    if (!new MetadataService()
                        .UseDatabaseProvider(Settings.DatabaseSettings.DatabaseProvider)
                        .UseConnectionString(Settings.DatabaseSettings.ConnectionString)
                        .TryOpenInfoBase(out InfoBase infoBase, out string error))
                    {
                        throw new Exception(error);
                    }

                    ExchangePlanHelper settings = new ExchangePlanHelper(
                        in infoBase,
                        Settings.DatabaseSettings.DatabaseProvider,
                        Settings.DatabaseSettings.ConnectionString);

                    settings.ConfigureSelectScripts("ПланОбмена.ПланОбменаДанными", "РегистрСведений.НастройкиОбменаРИБ");
                    
                    queues = settings.GetIncomingQueueNames();

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