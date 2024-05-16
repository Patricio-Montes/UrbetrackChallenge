using MediatR;
using NetChallenge.Application.CQRS.Bookings.Responses;
using System.Collections.Generic;

namespace NetChallenge.Application.CQRS.Bookings.Read.GetAll
{
    public record GetAllBookingsQuery() : IRequest<IEnumerable<BookingResponse>>;
}
