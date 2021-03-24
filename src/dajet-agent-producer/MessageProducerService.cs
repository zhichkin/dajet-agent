using DaJet.Metadata;
using DaJet.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DaJet.Agent.Producer
{
    public sealed class MessageProducerService : BackgroundService
    {
        private const string LOG_TOKEN = "P-SVC";
        private const string START_PROCESSING_OUTGOING_MESSAGES_MESSAGE = "Start processing outgoing messages ...";
        private const string OUTGOING_MESSAGES_PROCESSED_MESSAGE_TEMPLATE = "{0} outgoing messages processed.";
        private const string DATABASE_NOTIFICATIONS_ARE_NOT_ENABLED_MESSAGE = "Database notifications are not enabled.";
        private const string START_AWAITING_DATABASE_NOTIFICATION_MESSAGE = "Start awaiting database notification ...";
        private const string CRITICAL_ERROR_DELAY_MESSAGE_TEMPLATE = "Critical error delay of {0} seconds started.";
        private IServiceProvider Services { get; set; }
        private MessageProducerSettings Settings { get; set; }

        public MessageProducerService(IServiceProvider serviceProvider, IOptions<MessageProducerSettings> options)
        {
            Settings = options.Value;
            Services = serviceProvider;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log(LOG_TOKEN, "Message producer service is started.");
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log(LOG_TOKEN, "Message producer service is stopping ...");
            // Do shutdown cleanup here (see HostOptions.ShutdownTimeout)
            FileLogger.Log(LOG_TOKEN, "Message producer service is stopped.");
            return base.StopAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Running the job in the background
            _ = Task.Run(async () => { await DoWork(stoppingToken); }, stoppingToken);
            // Return completed task to let other services to run
            return Task.CompletedTask;
        }
        private async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    ConsumeMessages();
                }
                catch(Exception error)
                {
                    FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error));
                    FileLogger.Log(LOG_TOKEN, string.Format(CRITICAL_ERROR_DELAY_MESSAGE_TEMPLATE, Settings.CriticalErrorDelay));
                    await Task.Delay(Settings.CriticalErrorDelay * 1000, stoppingToken);
                    continue;
                }

                if (Settings.DatabaseSettings.UseNotifications
                    && Settings.DatabaseSettings.DatabaseProvider == DatabaseProviders.SQLServer
                    && !string.IsNullOrWhiteSpace(Settings.DatabaseSettings.NotificationQueueName))
                {
                    try
                    {
                        AwaitNotification(Settings.DatabaseSettings.WaitForNotificationTimeout * 1000);
                    }
                    catch
                    {
                        FileLogger.Log(LOG_TOKEN, DATABASE_NOTIFICATIONS_ARE_NOT_ENABLED_MESSAGE);
                        await Task.Delay(Settings.DatabaseSettings.DatabaseQueryingPeriodicity * 1000, stoppingToken);
                    }
                }
                else
                {
                    await Task.Delay(Settings.DatabaseSettings.DatabaseQueryingPeriodicity * 1000, stoppingToken);
                }
            }
        }
        private void ConsumeMessages()
        {
            int sumReceived = 0;
            int messagesReceived = 0;

            FileLogger.Log(LOG_TOKEN, START_PROCESSING_OUTGOING_MESSAGES_MESSAGE);

            IDatabaseMessageConsumer consumer = Services.GetService<IDatabaseMessageConsumer>();

            int messagesPerTransaction = Settings.DatabaseSettings.MessagesPerTransaction;
            do
            {
                messagesReceived = consumer.ConsumeMessages(messagesPerTransaction);
                sumReceived += messagesReceived;
            }
            while (messagesReceived > 0);
            
            FileLogger.Log(LOG_TOKEN, string.Format(OUTGOING_MESSAGES_PROCESSED_MESSAGE_TEMPLATE, sumReceived));
        }
        private void AwaitNotification(int timeout)
        {
            FileLogger.Log(LOG_TOKEN, START_AWAITING_DATABASE_NOTIFICATION_MESSAGE);

            IDatabaseMessageConsumer consumer = Services.GetService<IDatabaseMessageConsumer>();

            consumer.AwaitNotification(timeout);
        }
    }
}