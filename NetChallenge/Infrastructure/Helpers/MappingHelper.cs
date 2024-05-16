using NetChallenge.Application.CQRS.Bookings.Create;
using NetChallenge.Application.CQRS.Locations.Create;
using NetChallenge.Application.CQRS.Offices.Create;
using NetChallenge.Dto.Input;


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
    }
}
