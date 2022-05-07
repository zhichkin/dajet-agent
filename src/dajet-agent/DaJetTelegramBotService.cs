using DaJet.Agent.Consumer;
using DaJet.Logging;
using DaJet.Telegram.Bot;
using DaJet.Vector;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DaJet.Agent.Service
{
    internal sealed class DaJetTelegramBotService : BackgroundService
    {
        private const string SHUTDOWN_MESSAGE = "Telegram bot service shutdown.";
        private readonly IDaJetTelegramBot _telegram;
        private readonly VectorService _vectorService;
        private readonly MessageConsumerSettings _consumerOptions;

        private CancellationToken _cancellationToken;
        private AppSettings Settings { get; set; }
        public DaJetTelegramBotService(IOptions<AppSettings> settings,
            IOptions<MessageConsumerSettings> consumerOptions)
        {
            Settings = settings.Value;

            _consumerOptions = consumerOptions.Value;

            if (Settings.UseTelegram && _consumerOptions.UseVectorService)
            {
                DaJetBotOptions options = new DaJetBotOptions()
                {
                    Token = Settings.BotToken,
                    Channel = Settings.BotChannel
                };
                _telegram = new DaJetTelegramBot(Options.Create(options));

                _vectorService = new VectorService(Options.Create(
                    new VectorServiceOptions()
                    {
                        ConnectionString = Path.Combine(Settings.AppCatalog, "consumer-vector.db")
                    }));
            }
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;

            return Task.Factory.StartNew(DoWork, TaskCreationOptions.LongRunning);
        }
        public override void Dispose()
        {
            base.Dispose();

            _telegram?.Dispose();
        }
        private void DoWork()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                try
                {
                    TryDoWork();

                    Task.Delay(TimeSpan.FromSeconds(Settings.RefreshTimeout)).Wait(_cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    // do nothing - the wait task has been canceled
                }
                catch (Exception error)
                {
                    FileLogger.Log($"[TelegramBot] {error.Message}");
                }
            }
            FileLogger.Log(SHUTDOWN_MESSAGE);
        }
        private void TryDoWork()
        {
            if (_telegram == null)
            {
                return;
            }

            if (_vectorService == null)
            {
                return;
            }

            int sent = 0;

            CollisionInfo collision = _vectorService.SelectCollision();

            while (collision.NewVector > 0)
            {
                _telegram.SendMessage(
                    $"COLLISION {collision.Timestamp:dd-MM-yyyy HH:mm:ss}\n" +
                    $"[{collision.Node}] {collision.Type}\n" +
                    $"({collision.NewVector}) < ({collision.OldVector})");

                sent++;

                collision = _vectorService.SelectCollision();
            }

            FileLogger.Log($"[TelegramBot] {sent} notifications.");
        }
    }
}