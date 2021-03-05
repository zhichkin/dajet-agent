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
        private IServiceProvider Services { get; set; }
        private MessageConsumerSettings Settings { get; set; }
        public MessageConsumerService(IServiceProvider serviceProvider, IOptions<MessageConsumerSettings> options)
        {
            Settings = options.Value;
            Services = serviceProvider;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Message consumer service is started.");
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            FileLogger.Log("Message consumer service is stopping ...");
            // Do shutdown cleanup here (see HostOptions.ShutdownTimeout)
            FileLogger.Log("Message consumer service is stopped.");
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
            bool startConsuming = true;
            IMessageConsumer consumer = Services.GetService<IMessageConsumer>();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (startConsuming)
                    {
                        FileLogger.Log("Start message consumer ...");
                        consumer.Consume();
                        startConsuming = false;
                        FileLogger.Log("Message consumer is started.");
                    }
                }
                catch (Exception error)
                {
                    consumer.Dispose();
                    startConsuming = true;
                    FileLogger.Log(ExceptionHelper.GetErrorText(error));
                }
                if (startConsuming)
                {
                    FileLogger.Log(string.Format("Critical error delay of {0} seconds started.", Settings.CriticalErrorDelay));
                    await Task.Delay(Settings.CriticalErrorDelay * 1000, stoppingToken);
                }
                else
                {
                    await Task.Delay(300000, stoppingToken);
                }
            }
            consumer.Dispose(); // TODO: move to StopAsync method
        }
    }
}