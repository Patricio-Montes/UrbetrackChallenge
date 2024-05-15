using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace NetChallenge.Application.Data
{
    public interface IApplicationPersistence
    {
        Task<IEnumerable<object>> GetAsync(string keyPrefix);
        Task AddAsync(object entity);
        Task UpdateAsync(object entity);
        Task DeleteAsync(object entity);
    }
}
