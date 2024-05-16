using NetChallenge.Domain;
using System.Collections.Generic;
using System;

namespace NetChallenge.Abstractions
{
    public interface IBookingRepository : IRepository<Booking>
    {
        List<Booking> GetBookingsByOffice(Guid officeId);
    }
}