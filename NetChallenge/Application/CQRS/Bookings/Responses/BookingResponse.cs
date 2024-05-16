using System;

namespace NetChallenge.Application.CQRS.Bookings.Responses
{
    public record BookingResponse(
        Guid Id,
        string LocationName,
        string OfficeName,
        DateTime DateTime,
        TimeSpan Duration,
        string UserName);
}
