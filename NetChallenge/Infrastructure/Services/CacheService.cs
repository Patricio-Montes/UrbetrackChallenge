using Microsoft.Extensions.Caching.Distributed;
using NetChallenge.Application.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetChallenge.Infrastructure.Services
{
    public class CacheService : ICacheService, IDisposable
    {
        private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            string? cachedValue = await _distributedCache.GetStringAsync(
                key,
                cancellationToken);

            if (cachedValue is null)
            {
                return null;
            }

            T? value = JsonConvert.DeserializeObject<T>(cachedValue);

            return value;
        }

        public async Task<List<T>> GetAllAsync<T>(string keyPrefix, CancellationToken cancellationToken = default) where T : class
        {
            var keys = CacheKeys.Keys.Where(k => k.StartsWith(keyPrefix)).ToList();

            var cachedValues = new List<T>();

            foreach (var key in keys)
            {
                var cachedValue = await GetAsync<T>(key, cancellationToken);
                if (cachedValue != null)
                {
                    cachedValues.Add(cachedValue);
                }
            }

            return cachedValues;
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        {
            string cacheValue = JsonConvert.SerializeObject(value);

            await _distributedCache.SetStringAsync(key, cacheValue, cancellationToken);

            CacheKeys.TryAdd(key, true);
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);

            CacheKeys.TryRemove(key, out bool _);
        }

        public void Clear(CancellationToken cancellationToken = default)
        {
            var keys = CacheKeys.Keys.ToList();

            foreach (var key in keys)
            {
                _distributedCache.RemoveAsync(key, cancellationToken);
                CacheKeys.TryRemove(key, out _);
            }
        }

        public void Dispose()
        {
            if (_distributedCache is IDisposable disposableCache)
            {
                disposableCache.Dispose();
            }
        }
    }
}
