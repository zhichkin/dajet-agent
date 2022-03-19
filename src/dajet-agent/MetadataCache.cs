using DaJet.Metadata;
using DaJet.Metadata.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace DaJet.Agent.Service
{
    internal interface IMetadataCache
    {
        void Refresh(out string error);
        bool TryGet(out InfoBase infoBase);
    }
    internal sealed class MetadataCache : IMetadataCache
    {
        private readonly IMemoryCache _cache;
        private const string CACHE_KEY = "METADATA";
        private readonly RWLockSlim _lock = new RWLockSlim();
        private DaJetAgentOptions Options { get; set; }
        public MetadataCache(IOptions<DaJetAgentOptions> options, IMemoryCache cache)
        {
            _cache = cache;
            Options = options.Value;
        }
        void IMetadataCache.Refresh(out string error)
        {
            using (_lock.WriteLock())
            {
                if (new MetadataService()
                    .UseDatabaseProvider(Options.DatabaseProvider)
                    .UseConnectionString(Options.ConnectionString)
                    .TryOpenInfoBase(out InfoBase infoBase, out error))
                {
                    _cache.Set(CACHE_KEY, infoBase);
                }
            }
        }
        bool IMetadataCache.TryGet(out InfoBase infoBase)
        {
            using (_lock.ReadLock())
            {
                return _cache.TryGetValue(CACHE_KEY, out infoBase);
            }
        }
    }
}