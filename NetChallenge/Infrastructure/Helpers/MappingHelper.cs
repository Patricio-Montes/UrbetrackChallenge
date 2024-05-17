using NetChallenge.Application.CQRS.Bookings.Create;
using NetChallenge.Application.CQRS.Bookings.Responses;
using NetChallenge.Application.CQRS.Locations.Create;
using NetChallenge.Application.CQRS.Locations.Responses;
using NetChallenge.Application.CQRS.Offices.Create;
using NetChallenge.Application.CQRS.Offices.Responses;
using NetChallenge.Dto.Input;
using NetChallenge.Dto.Output;
using System.Collections.Generic;
using System.Linq;


namespace NetChallenge.Infrastructure.Helpers
{
    internal static class MappingHelper
    {
        internal static CreateLocationCommand MapToAddLocationCommand(AddLocationRequest request)
        {
            return new CreateLocationCommand(request.Name, request.Neighborhood);
        }

        internal static CreateOfficeCommand MapToAddOfficeCommand(AddOfficeRequest request)
        {
            return new CreateOfficeCommand(
                request.LocationName,
                request.Name,
                request.MaxCapacity,
                request.AvailableResources
            );
        }

        internal static CreateBookingCommand MapToAddBookingCommand(BookOfficeRequest request)
        {
            return new CreateBookingCommand(
                request.LocationName,
                request.OfficeName,
                request.DateTime,
                request.Duration,
                request.UserName
            );
        }

        public static IEnumerable<LocationDto> MapToLocationDtos(IEnumerable<LocationResponse> locationResponses)
        {
            return locationResponses.Select(locationResponse => new LocationDto
            {
                Name = locationResponse.Name,
                Neighborhood = locationResponse.Neighborhood
            });
        }

        public static IEnumerable<BookingDto> MapToBookingDtos(IEnumerable<BookingResponse> bookingResponses)
        {
            return bookingResponses.Select(bookingResponse => new BookingDto
            {
                LocationName = bookingResponse.LocationName,
                OfficeName = bookingResponse.OfficeName,
                DateTime = bookingResponse.DateTime,
                Duration = bookingResponse.Duration,
                UserName = bookingResponse.UserName
            });
        }

        public static IEnumerable<OfficeDto> MapToOfficeDtos(IEnumerable<OfficeResponse> officeResponses)
        {
            return officeResponses.Select(officeResponse => new OfficeDto
            {
                LocationName = officeResponse.LocationName,
                Name = officeResponse.Name,
                MaxCapacity = officeResponse.MaxCapacity,
                AvailableResources = officeResponse.AvailableResources.ToArray()
            });
        }
    }
}
