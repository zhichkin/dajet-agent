using DaJet.Utilities;
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
            FileLogger.Log("Worker is started.");
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Worker is stoped.");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ReceiveMessages(out string errorMessage);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    FileLogger.Log(errorMessage);
                    FileLogger.Log(string.Format("Critical error delay of {0} seconds started.", Settings.CriticalErrorDelay / 1000));
                    await Task.Delay(Settings.CriticalErrorDelay, stoppingToken);
                }

                int resultCode = AwaitNotification(Settings.WaitForNotificationTimeout);
                if (resultCode == 1) // notifications are not supported by database
                {
                    await Task.Delay(Settings.ReceivingMessagesPeriodicity, stoppingToken);
                }
            }
        }
        private void ReceiveMessages(out string errorMessage)
        {
            int sumReceived = 0;
            int messagesReceived = 0;
            errorMessage = string.Empty;

            FileLogger.Log("Start receiving messages.");

            try
            {
                IMessageConsumer consumer = Services.GetService<IMessageConsumer>();
                messagesReceived = consumer.ReceiveMessages(Settings.MessagesPerTransaction, out errorMessage);
                sumReceived += messagesReceived;
                while (messagesReceived > 0)
                {
                    messagesReceived = consumer.ReceiveMessages(Settings.MessagesPerTransaction, out errorMessage);
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
                IMessageConsumer consumer = Services.GetService<IMessageConsumer>();
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
