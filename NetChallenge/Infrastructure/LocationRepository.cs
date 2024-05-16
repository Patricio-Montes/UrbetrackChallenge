using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetChallenge.Abstractions;
using NetChallenge.Application.Data;
using NetChallenge.Domain;
using NetChallenge.Infrastructure.Helpers;

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
            return GetAllAsync().Result;
        }

        public async Task<List<Location>> GetAllAsync()
        {
            var result = await _persistence.GetAsync("Location");

            if (result is null || !result.Any())
            {
                return new List<Location>();
            }

            return SerializationHelper.DeserializeList<Location>(result);
        }

        public async Task Add(Location item)
        {
            await _persistence.AddAsync(item);
        }

        public async Task<Location> GetByName(string name)
        {
            var result = await _persistence.GetAsync("Location");

            if (result is null || !result.Any())
            {
                return null;
            }

            var locationListResults = SerializationHelper.DeserializeList<Location>(result);

            return locationListResults.FirstOrDefault(loc =>
                loc.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}