using NetChallenge.Domain;
using System.Threading.Tasks;

namespace NetChallenge.Abstractions
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<Location> GetByName(string name);
    }
}