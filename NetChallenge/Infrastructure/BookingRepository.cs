using System.Collections.Generic;
using System.Threading.Tasks;
using NetChallenge.Abstractions;
using NetChallenge.Domain;

namespace NetChallenge.Infrastructure
{
    public class BookingRepository : IBookingRepository
    {
        public IEnumerable<Booking> AsEnumerable()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Booking>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task Add(Booking item)
        {
            throw new System.NotImplementedException();
        }
    }
}