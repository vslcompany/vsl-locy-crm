using Microsoft.Extensions.Caching.Memory;
using vsl_crm_api.Interfaces;

namespace vsl_crm_api.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _defaultCacheDuration = TimeSpan.FromMinutes(1);

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void SetValue(string key, string value, TimeSpan? expiration = null)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? _defaultCacheDuration
            };
            _memoryCache.Set(key, value, cacheEntryOptions);
        }

        public string? GetValue(string key)
        {
            string? result = _memoryCache.TryGetValue(key, out string? value) ? value : null;
            return result;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
