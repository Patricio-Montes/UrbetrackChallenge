using NetChallenge.Domain;

namespace NetChallenge.Abstractions
{
    public interface ILocationRepository : IRepository<Location>
    {
        Location GetByName(string name);
    }
}