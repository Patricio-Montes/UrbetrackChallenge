using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetChallenge.Abstractions;
using NetChallenge.Application.Data;
using NetChallenge.Domain;

namespace NetChallenge.Infrastructure
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IApplicationPersistence _persistence;

        public LocationRepository(IApplicationPersistence persistence)
        {
            _persistence = persistence ?? throw new ArgumentNullException(nameof(persistence));
        }

        public IEnumerable<Location> AsEnumerable()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return (IEnumerable<Location>) await _persistence.GetAsync("Location");
        }

        public async Task Add(Location item)
        {
            await _persistence.AddAsync(item);
        }
    }
}