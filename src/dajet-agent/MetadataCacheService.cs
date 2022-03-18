using DaJet.Logging;
using DaJet.Metadata;
using DaJet.Metadata.Model;
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
        private DaJetAgentOptions Options { get; set; }
        public MetadataCacheService(IOptions<DaJetAgentOptions> options, IMetadataCache cache)
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
        }
        private void TryDoWork()
        {
            if (new MetadataService()
                .UseDatabaseProvider(Options.DatabaseProvider)
                .UseConnectionString(Options.ConnectionString)
                .TryOpenInfoBase(out InfoBase infoBase, out string error))
            {
                _cache.Set(in infoBase);

                FileLogger.Log($"[MetadataCacheService] Metadata cache updated successfully.");
            }
            else
            {
                FileLogger.Log($"[MetadataCacheService] Failed to update metadata cache: {error}");
            }
        }
    }
}