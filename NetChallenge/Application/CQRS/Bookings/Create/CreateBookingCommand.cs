using MediatR;
using System;

namespace NetChallenge.Application.CQRS.Bookings.Create
{
    public record CreateBookingCommand(
        string LocationName,
        string OfficeName,
        DateTime DateTime,
        TimeSpan Duration,
        string UserName) : IRequest<Unit>;
}
