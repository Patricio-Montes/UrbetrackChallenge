using NetChallenge.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace NetChallenge.Abstractions
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<List<Booking>> GetBookingsByOffice(Guid officeId);
    }
}