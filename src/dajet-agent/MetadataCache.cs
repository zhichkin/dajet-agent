using DaJet.Metadata.Model;
using Microsoft.Extensions.Caching.Memory;

namespace DaJet.Agent.Service
{
    internal interface IMetadataCache
    {
        void Set(in InfoBase infoBase);
        bool TryGet(out InfoBase infoBase);
    }
    internal sealed class MetadataCache : IMetadataCache
    {
        private readonly IMemoryCache _cache;
        private const string CACHE_KEY = "METADATA";
        private readonly object _lock = new object();
        public MetadataCache(IMemoryCache cache)
        {
            _cache = cache;
        }
        void IMetadataCache.Set(in InfoBase infoBase)
        {
            lock (_lock)
            {
                _cache.Set(CACHE_KEY, infoBase);
            }
        }
        bool IMetadataCache.TryGet(out InfoBase infoBase)
        {
            lock (_lock)
            {
                return _cache.TryGetValue(CACHE_KEY, out infoBase);
            }
        }
    }
}