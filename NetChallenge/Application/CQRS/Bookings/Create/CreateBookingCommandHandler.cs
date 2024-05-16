using MediatR;
using NetChallenge.Abstractions;
using NetChallenge.Domain;
using NetChallenge.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetChallenge.Application.CQRS.Bookings.Create
{
    internal class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Unit>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IOfficeRepository _officeRepository;

        public CreateBookingCommandHandler(IBookingRepository bookingRepository, ILocationRepository locationRepository, IOfficeRepository officeRepository)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
            _officeRepository = officeRepository ?? throw new ArgumentNullException(nameof(officeRepository));
        }

        public async Task<Unit> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            ValidateBusinessRules(request);

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                Office = GetOffice(request.LocationName, request.OfficeName),
                Datetime = request.DateTime,
                Duration = request.Duration,
                User = new User { Id = Guid.NewGuid(), Name = request.UserName },
            };

            await _bookingRepository.Add(booking);

            return Unit.Value;
        }
        
        private void ValidateBusinessRules(CreateBookingCommand request)
        {
            if (string.IsNullOrEmpty(request.UserName))
            {
                throw new InvalidFieldException("UserName");
            }

            if (request.Duration <= TimeSpan.Zero)
            {
                throw new ValidationException("Duration must be greater than zero.");
            }

            var location = GetLocation(request.LocationName);
            if (location is null)
            {
                throw new ValidationException($"Location '{request.LocationName}' not found.");
            }

            var office = GetOffice(request.LocationName, request.OfficeName);
            if (office is null)
            {
                throw new ValidationException($"Office '{request.OfficeName}' doesn't exists.");
            }

            if (IsOfficeBooked(office, request.DateTime, request.Duration))
            {
                throw new ValidationException($"Office '{request.OfficeName}' is already booked at the requested time.");
            }
        }

        private Office GetOffice(string locationName, string officeName)
        {
            return _officeRepository
                 .Get(locationName).Result
                 .Where(o => o.Name == officeName)
                 .FirstOrDefault();
        }

        private Location GetLocation(string locationName)
        {
            return _locationRepository.GetByName(locationName).Result;
        }

        private bool IsOfficeBooked(Office office, DateTime startTime, TimeSpan duration)
        {
            var endTime = startTime.Add(duration);
            var existingBookings = _bookingRepository.GetBookingsByOffice(office.Id).Result;

            foreach (var booking in existingBookings)
            {
                var bookingEndTime = booking.Datetime.Add(booking.Duration);
                if (startTime < bookingEndTime && endTime > booking.Datetime)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
