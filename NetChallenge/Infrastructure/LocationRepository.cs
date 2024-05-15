using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetChallenge.Abstractions;
using NetChallenge.Application.Data;
using NetChallenge.Domain;
using Newtonsoft.Json;

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

        public async Task<List<Location>> GetAllAsync()
        {
            var result = await _persistence.GetAsync("Location");

            var locationListResults = result.Select(item =>
            {
                // Deserializa cada elemento a un objeto de tipo Location
                var locationJson = JsonConvert.SerializeObject(item);
                return JsonConvert.DeserializeObject<Location>(locationJson);
            }).ToList();

            return locationListResults;
        }

        public async Task Add(Location item)
        {
            await _persistence.AddAsync(item);
        }
    }
}