using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetChallenge.Abstractions
{
    public interface IRepository<T>
    {
        IEnumerable<T> AsEnumerable();
        Task<List<T>> GetAllAsync();
        Task Add(T item);
    }
}