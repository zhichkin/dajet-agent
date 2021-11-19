using DaJet.Database.Adapter;
using DaJet.Metadata;
using DaJet.Metadata.Model;
using DaJet.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DaJet.Agent.Consumer
{
    public sealed class MessageConsumerService : BackgroundService
    {
        private const string LOG_TOKEN = "C-SVC";
        private const string SERVICE_HEARTBEAT_MESSAGE = "Message consumer service heartbeat.";
        private const string CRITICAL_ERROR_DELAY_TEMPLATE = "Consumer critical error delay of {0} seconds started.";
        private IServiceProvider Services { get; set; }
        private MessageConsumerSettings Settings { get; set; }
        private IMessageConsumer MessageConsumer { get; set; }
        public MessageConsumerService(IServiceProvider serviceProvider, IOptions<MessageConsumerSettings> options)
        {
            Settings = options.Value;
            Services = serviceProvider;
            MessageConsumer = Services.GetService<IMessageConsumer>();
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                ValidateAndConfigureDatabaseInterface();
            }
            catch (Exception error)
            {
                FileLogger.Log(LOG_TOKEN, "Message consumer service failed to start:\n" + ExceptionHelper.GetErrorText(error));
                return Task.CompletedTask;
            }

            FileLogger.Log(LOG_TOKEN, "Message consumer service is started.");
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log(LOG_TOKEN, "Message consumer service is stopping ...");
            try
            {
                MessageConsumer.Dispose();
            }
            catch (Exception error)
            {
                FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error));
            }
            FileLogger.Log(LOG_TOKEN, "Message consumer service is stopped.");
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
                bool IsInCriticalErrorState = false;
                try
                {
                    if (Settings.MessageBrokerSettings.ConsumeMode == 0)
                    {
                        MessageConsumer.PushConsume(); // eventing consumer (push api)
                    }
                    else
                    {
                        MessageConsumer.PullConsume(stoppingToken); // basic get consumer (pull api)
                    }
                    FileLogger.Log(LOG_TOKEN, SERVICE_HEARTBEAT_MESSAGE);
                }
                catch (Exception error)
                {
                    IsInCriticalErrorState = true;
                    FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error)
                        + Environment.NewLine
                        + string.Format(CRITICAL_ERROR_DELAY_TEMPLATE, Settings.CriticalErrorDelay));
                }
                if (IsInCriticalErrorState)
                {
                    await Task.Delay(Settings.CriticalErrorDelay * 1000, stoppingToken);
                }
                else
                {
                    await Task.Delay(180000, stoppingToken);
                }
            }
        }

        private void ValidateAndConfigureDatabaseInterface()
        {
            IDatabaseConfigurator configurator = Services.GetService<IDatabaseConfigurator>();
            configurator
                .UseDatabaseProvider(Settings.DatabaseSettings.DatabaseProvider)
                .UseConnectionString(Settings.DatabaseSettings.ConnectionString);

            if (!configurator.TryOpenInfoBase(out InfoBase infoBase, out string errorMessage))
            {
                throw new Exception($"Failed to load 1C metadata:\n{errorMessage}");
            }

            ApplicationObject queue = configurator.GetIncomingQueueMetadata(infoBase);
            if (queue == null)
            {
                throw new Exception($"Failed to load 1C metadata for the incoming queue.");
            }

            if (configurator.IncomingQueueSequenceExists())
            {
                // Version 5.0.0. TODO: Remove in future versions!
                configurator.DropIncomingQueueTrigger(queue);
            }
            else
            {
                configurator.ConfigureIncomingQueue(queue);
            }
        }
    }
}