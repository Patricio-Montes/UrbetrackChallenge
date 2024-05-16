using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = _persistence.GetAsync("Office").Result;

            if (result is null || !result.Any())
            {
                return new List<Office>();
            }

            return SerializationHelper.DeserializeList<Office>(result);
        }

        public void Add(Office item)
        {
            _persistence.AddAsync(item);
        }

        public List<Office> Get(string locationName)
        {
            return AsEnumerable()
                   .Where(office => office.Location.Name.Equals(locationName, StringComparison.OrdinalIgnoreCase))
                   .ToList();
        }
    }
}