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
        private IServiceProvider Services { get; set; }
        private MessageProducerSettings Settings { get; set; }

        public MessageProducerService(IServiceProvider serviceProvider, IOptions<MessageProducerSettings> options)
        {
            Settings = options.Value;
            Services = serviceProvider;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Message producer service is started.");
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Message producer service is stopping ...");
            // Do shutdown cleanup here (see HostOptions.ShutdownTimeout)
            FileLogger.Log("Message producer service is stopped.");
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
                ConsumeMessages(out string errorMessage);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    FileLogger.Log(errorMessage);
                    FileLogger.Log(string.Format("Critical error delay of {0} seconds started.", Settings.CriticalErrorDelay));
                    await Task.Delay(Settings.CriticalErrorDelay * 1000, stoppingToken);
                }
                await Task.Delay(Settings.DatabaseSettings.DatabaseQueryingPeriodicity * 1000, stoppingToken);

                //if (Settings.DatabaseSettings.DatabaseProvider == DatabaseProviders.SQLServer)
                //{
                //    int resultCode = AwaitNotification(Settings.DatabaseSettings.WaitForNotificationTimeout * 1000);
                //    if (resultCode == 1) // notifications are not supported by database
                //    {
                //        await Task.Delay(Settings.DatabaseSettings.DatabaseQueryingPeriodicity * 1000, stoppingToken);
                //    }
                //}
                //else
                //{
                //    await Task.Delay(Settings.DatabaseSettings.DatabaseQueryingPeriodicity * 1000, stoppingToken);
                //}
            }
        }
        private void ConsumeMessages(out string errorMessage)
        {
            int sumReceived = 0;
            int messagesReceived = 0;
            errorMessage = string.Empty;

            FileLogger.Log("Start receiving messages.");

            try
            {
                int messagesPerTransaction = Settings.DatabaseSettings.MessagesPerTransaction;
                IDatabaseMessageConsumer consumer = Services.GetService<IDatabaseMessageConsumer>();
                messagesReceived = consumer.ConsumeMessages(messagesPerTransaction, out errorMessage);
                sumReceived += messagesReceived;
                while (messagesReceived > 0)
                {
                    messagesReceived = consumer.ConsumeMessages(messagesPerTransaction, out errorMessage);
                    sumReceived += messagesReceived;
                }
            }
            catch (Exception error)
            {
                errorMessage += (string.IsNullOrEmpty(errorMessage) ? string.Empty : Environment.NewLine)
                    + ExceptionHelper.GetErrorText(error);
            }

            FileLogger.Log(string.Format("{0} messages received.", sumReceived));
        }
        private int AwaitNotification(int timeout)
        {
            int resultCode = 0;
            string errorMessage = string.Empty;

            FileLogger.Log("Start awaiting notification ...");

            try
            {
                IDatabaseMessageConsumer consumer = Services.GetService<IDatabaseMessageConsumer>();
                resultCode = consumer.AwaitNotification(timeout, out errorMessage);
            }
            catch (Exception error)
            {
                errorMessage += (string.IsNullOrEmpty(errorMessage) ? string.Empty : Environment.NewLine)
                    + ExceptionHelper.GetErrorText(error);
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                FileLogger.Log(errorMessage);
            }

            if (resultCode == 0)
            {
                FileLogger.Log("Notification received successfully.");
            }
            else if (resultCode == 1)
            {
                FileLogger.Log("Notifications are not supported.");
            }
            else if (resultCode == 2)
            {
                FileLogger.Log("No notification received.");
            }

            return resultCode;
        }
    }
}
