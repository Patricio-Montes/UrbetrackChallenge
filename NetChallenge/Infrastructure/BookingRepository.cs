using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetChallenge.Abstractions;
using NetChallenge.Application.Data;
using NetChallenge.Domain;

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
            throw new System.NotImplementedException();
        }

        public Task<List<Booking>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task Add(Booking item)
        {
            throw new System.NotImplementedException();
        }
    }
}