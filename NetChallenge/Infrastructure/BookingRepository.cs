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
            return GetAllAsync().Result;
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            var result = await _persistence.GetAsync("Booking");

            if (result is null || !result.Any())
            {
                return new List<Booking>();
            }

            return SerializationHelper.DeserializeList<Booking>(result);
        }

        public async Task Add(Booking item)
        {
            await _persistence.AddAsync(item);
        }

        public async Task<List<Booking>> GetBookingsByOffice(Guid officeId)
        {
            var allBookings = await GetAllAsync();
            return allBookings.Where(b => b.Office.Id == officeId).ToList();
        }
    }
}