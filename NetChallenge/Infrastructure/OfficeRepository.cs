using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using NetChallenge.Abstractions;
using NetChallenge.Application.Data;
using NetChallenge.Domain;
using NetChallenge.Infrastructure.Helpers;

namespace NetChallenge.Infrastructure
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly IApplicationPersistence _persistence;

        public OfficeRepository(IApplicationPersistence persistence)
        {
            _persistence = persistence ?? throw new System.ArgumentNullException(nameof(persistence));
        }

        public IEnumerable<Office> AsEnumerable()
        {
            return GetAllAsync().Result;
        }

        public async Task<List<Office>> GetAllAsync()
        {
            var result = await _persistence.GetAsync("Office");

            if (result is null || !result.Any())
            {
                return new List<Office>();
            }

            return SerializationHelper.DeserializeList<Office>(result);
        }

        public async Task Add(Office item)
        {
            await _persistence.AddAsync(item);
        }

        public async Task<List<Office>> Get(string locationName)
        {
            var result = await _persistence.GetAsync("Office");

            if (result is null || !result.Any())
            {
                return new List<Office>();
            }

            var officeListResults = SerializationHelper.DeserializeList<Office>(result);

            return officeListResults.Where(office =>
                   office.Location.Name.Equals(locationName, StringComparison.OrdinalIgnoreCase))
                   .ToList();
        }
    }
}