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
    public class BookingRepository : IBookingRepository
    {
        private readonly IApplicationPersistence _persistence;

        public BookingRepository(IApplicationPersistence persistence)
        {
            _persistence = persistence ?? throw new ArgumentNullException(nameof(persistence));
        }

        public IEnumerable<Booking> AsEnumerable()
        {
            var result = _persistence.GetAsync("Booking").Result;

            if (result is null || !result.Any())
            {
                return Enumerable.Empty<Booking>();
            }

            return SerializationHelper.DeserializeList<Booking>(result);
        }

        public void Add(Booking item)
        {
            _persistence.AddAsync(item);
        }

        public List<Booking> GetBookingsByOffice(Guid officeId)
        {
            return AsEnumerable()
                    .Where(b => b.Office.Id == officeId)
                    .ToList();
        }
    }
}