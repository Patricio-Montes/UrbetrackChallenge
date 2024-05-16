using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = _persistence.GetAsync("Location").Result;

            if (result is null || !result.Any())
            {
                return new List<Location>();
            }

            return SerializationHelper.DeserializeList<Location>(result);
        }

        public void Add(Location item)
        {
            _persistence.AddAsync(item);
        }

        public Location GetByName(string name)
        {
            return AsEnumerable().FirstOrDefault(loc =>
                loc.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}