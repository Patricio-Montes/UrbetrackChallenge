using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetChallenge.Application.Data;
using NetChallenge.Application.Services;

namespace NetChallenge.Infrastructure.Persistence
{
    public class ApplicationPersistence : IApplicationPersistence
    {
        private readonly ICacheService _cache;

        public ApplicationPersistence(ICacheService cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<IEnumerable<object>> GetAsync(string keyPrefix)
        {
            var cachedItems = await _cache.GetAllAsync<object>(keyPrefix, default);

            return cachedItems;
        }

        public async Task AddAsync(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entityType = entity.GetType();
            var id = GetObjectId(entityType, entity);

            var key = $"{entityType.Name}_{id}";

            await _cache.SetAsync(key, entity);
        }

        public async Task UpdateAsync(object entity)
        {
            await AddAsync(entity);
        }

        public async Task DeleteAsync(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entityType = entity.GetType();
            var id = GetObjectId(entityType, entity);

            var key = $"{entityType.Name}_{id}";
            await _cache.RemoveAsync(key);
        }

        private Guid GetObjectId(Type entityType, object entity)
        {
            var property = entityType.GetProperty("Id");
            if (property == null)
            {
                throw new ArgumentException("The Id property was not found in the entity.", nameof(entity));
            }

            return (Guid)property.GetValue(entity);
        }

        public void Dispose()
        {
            // Implementa la disposición de recursos si es necesario
        }
    }
}
