using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetChallenge.Application.Services;
using Newtonsoft.Json;

namespace NetChallenge.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly Dictionary<string, string> _cache = new Dictionary<string, string>();
        private readonly HashSet<string> _cacheKeys = new HashSet<string>();

        public Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            if (_cache.TryGetValue(key, out string cachedValue))
            {
                return Task.FromResult(JsonConvert.DeserializeObject<T>(cachedValue));
            }
            return Task.FromResult<T>(null);
        }

        public async Task<List<T>> GetAllAsync<T>(string keyPrefix, CancellationToken cancellationToken = default) where T : class
        {
            var cachedValues = new List<T>();

            foreach (var kvp in _cache)
            {
                if (kvp.Key.StartsWith(keyPrefix))
                {
                    var cachedValue = JsonConvert.DeserializeObject<T>(kvp.Value);
                    if (cachedValue != null)
                    {
                        cachedValues.Add(cachedValue);
                    }
                }
            }

            return cachedValues;
        }

        public Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        {
            string cacheValue = JsonConvert.SerializeObject(value);
            _cache[key] = cacheValue;
            _cacheKeys.Add(key);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            _cache.Remove(key);
            _cacheKeys.Remove(key);
            return Task.CompletedTask;
        }

        public void Clear(CancellationToken cancellationToken = default)
        {
            _cache.Clear();
            _cacheKeys.Clear();
        }
    }
}