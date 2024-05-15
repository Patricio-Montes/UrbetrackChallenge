using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace NetChallenge.Application.Data
{
    public interface IApplicationPersistence<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

        Task<TEntity> GetFromCacheAsync(string key);
        Task AddToCacheAsync(string key, TEntity entity, TimeSpan? expiry = null);
        Task RemoveFromCacheAsync(string key);
    }
}
