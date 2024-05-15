using System.Threading;
using System.Threading.Tasks;

namespace NetChallenge.Domain.Primitives
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
