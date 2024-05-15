using System.Collections.Generic;
using System.Threading.Tasks;
using NetChallenge.Abstractions;
using NetChallenge.Application.Data;
using NetChallenge.Domain;

namespace NetChallenge.Infrastructure
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IApplicationPersistence<Location> _persistence;

        public LocationRepository(IApplicationPersistence<Location> persistence)
        {
            _persistence = persistence ?? throw new System.ArgumentNullException(nameof(persistence));
        }

        public IEnumerable<Location> AsEnumerable()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _persistence.GetAllAsync();
        }

        public async Task Add(Location item)
        {
            await _persistence.AddAsync(item);
        }
    }
}