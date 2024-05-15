using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetChallenge.Abstractions
{
    public interface IRepository<T>
    {
        IEnumerable<T> AsEnumerable();
        Task<IEnumerable<T>> GetAllAsync();
        Task Add(T item);
    }
}