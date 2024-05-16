using MediatR;
using NetChallenge.Abstractions;
using NetChallenge.Application.CQRS.Bookings.Responses;
using NetChallenge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetChallenge.Application.CQRS.Bookings.Read.GetAll
{
    public sealed class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, IEnumerable<BookingResponse>>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetAllBookingsQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        }

        public async Task<IEnumerable<BookingResponse>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            var bookings = _bookingRepository.AsEnumerable();

            if (bookings is null || !bookings.Any())
            {
                return Enumerable.Empty<BookingResponse>();
            }

            return bookings.Select(booking => MapToBookingResponse(booking));
        }

        private BookingResponse MapToBookingResponse(Booking booking)
        {
            return new BookingResponse(
                booking.Id,
                booking.Office.Location.Name,
                booking.Office.Name,
                booking.Datetime,
                booking.Duration,
                booking.User.Name
            );
        }
    }


}
