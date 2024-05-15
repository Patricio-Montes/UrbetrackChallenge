using System.Collections.Generic;
using System.Threading.Tasks;
using NetChallenge.Abstractions;
using NetChallenge.Application.Data;
using NetChallenge.Domain;

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
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Office>> GetAllAsync()
        {
            return (IEnumerable<Office>) await _persistence.GetAsync("Location");
        }

        public async Task Add(Office item)
        {
            await _persistence.AddAsync(item);
        }
    }
}