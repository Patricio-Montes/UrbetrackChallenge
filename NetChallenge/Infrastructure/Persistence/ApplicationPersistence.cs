using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using NetChallenge.Application.Data;

namespace NetChallenge.Infrastructure.Persistence
{
    internal class ApplicationPersistence<TEntity> : IApplicationPersistence<TEntity> where TEntity : class
    {
        private readonly IMemoryCache _cache;

        public ApplicationPersistence(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            if (_cache.TryGetValue(id, out TEntity entity))
            {
                return entity;
            }

            return await Task.FromResult<TEntity>(null);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue("AllItems", out IEnumerable<TEntity> items))
            {
                return items;
            }

            // Si no están en la caché, se deben recuperar de la fuente de datos principal,
            // y luego almacenar en la caché para usos futuros.
            // Por ahora, retornemos una lista vacía como ejemplo.
            return await Task.FromResult<IEnumerable<TEntity>>(new List<TEntity>());
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            // Implement the logic to add the object to the cache
            int id = GetObjectId(entity);
            _cache.Set(id, entity);
            return await Task.FromResult(entity);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            // Implement the logic to update the object in the cache
            int id = GetObjectId(entity);
            _cache.Set(id, entity);
            return await Task.FromResult(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            // Implement the logic to remove the object from the cache
            int id = GetObjectId(entity);
            _cache.Remove(id);
            await Task.CompletedTask;
        }

        public async Task<TEntity> GetFromCacheAsync(string key)
        {
            if (_cache.TryGetValue(key, out TEntity entity))
            {
                return entity;
            }

            return await Task.FromResult<TEntity>(null);
        }

        public async Task AddToCacheAsync(string key, TEntity entity, TimeSpan? expiry = null)
        {
            _cache.Set(key, entity, expiry ?? TimeSpan.FromSeconds(60)); // Example expiry of 60 seconds
            await Task.CompletedTask;
        }

        public async Task RemoveFromCacheAsync(string key)
        {
            _cache.Remove(key);
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            // Implement the logic to release resources if necessary
        }

        // This helper method is just an example. Implement it according to your actual needs.
        private int GetObjectId(TEntity entity)
        {
            // Implement the logic to get the ID of the object
            // This can vary depending on your entity structure
            // This method is just a schema of how you could do it
            return 0;
        }
    }
}