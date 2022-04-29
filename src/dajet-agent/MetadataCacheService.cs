using DaJet.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DaJet.Agent.Service
{
    internal sealed class MetadataCacheService : BackgroundService
    {
        private readonly IMetadataCache _cache;
        private CancellationToken _cancellationToken;
        private AppSettings Options { get; set; }
        public MetadataCacheService(IMetadataCache cache, IOptions<AppSettings> options)
        {
            _cache = cache;
            Options = options.Value;
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;

            return Task.Factory.StartNew(DoWork, TaskCreationOptions.LongRunning);
        }
        private void DoWork()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                try
                {
                    TryDoWork();

                    Task.Delay(TimeSpan.FromSeconds(Options.RefreshTimeout)).Wait(_cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    // do nothing - the wait task has been canceled
                }
                catch (Exception error)
                {
                    FileLogger.LogException(error);
                }
            }

            FileLogger.Log("[MetadataCacheService] Shutdown.");
        }
        private void TryDoWork()
        {
            FileLogger.Log("[MetadataCacheService] Updating metadata cache ...");

            _cache.Refresh(out string error); // Initialize or refresh metadata cache

            if (string.IsNullOrEmpty(error))
            {
                FileLogger.Log("[MetadataCacheService] Metadata cache updated successfully.");
            }
            else
            {
                FileLogger.Log($"[MetadataCacheService] Failed to update metadata cache.\n{error}");
            }
        }
    }
}