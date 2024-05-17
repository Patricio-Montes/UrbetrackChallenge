using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using NetChallenge.Application.CQRS.Bookings.Read.GetAll;
using NetChallenge.Application.CQRS.Locations.Read.GetAll;
using NetChallenge.Application.CQRS.Offices.Read;
using NetChallenge.Application.CQRS.Offices.Read.GetAll;
using NetChallenge.Application.Services;
using NetChallenge.Dto.Input;
using NetChallenge.Dto.Output;
using NetChallenge.Infrastructure.Helpers;

namespace NetChallenge
{
    public class OfficeRentalService : IOfficeRentalService
    {
        private readonly IMediator _mediator;

        public OfficeRentalService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public void AddLocation(AddLocationRequest request)
        {
            try
            {
                var command = MappingHelper.MapToAddLocationCommand(request);
                _mediator.Send(command).Wait();
            }
            catch (AggregateException ex)
            {
                throw ex.InnerExceptions.FirstOrDefault();
            }
        }

        public void AddOffice(AddOfficeRequest request)
        {
            try
            {
                var command = MappingHelper.MapToAddOfficeCommand(request);
                _mediator.Send(command).Wait();
            }
            catch (AggregateException ex)
            {
                throw ex.InnerExceptions.FirstOrDefault();
            }
        }

        public void BookOffice(BookOfficeRequest request)
        {
            try
            {
                var command = MappingHelper.MapToAddBookingCommand(request);
                _mediator.Send(command).Wait();
            }
            catch (AggregateException ex)
            {
                throw ex.InnerExceptions.FirstOrDefault();
            }
        }

        public IEnumerable<BookingDto> GetBookings(string locationName, string officeName)
        {
            var bookingsResponse = _mediator.Send(new GetAllBookingsQuery()).Result;
            return MappingHelper.MapToBookingDtos(bookingsResponse);
        }

        public IEnumerable<LocationDto> GetLocations()
        {
            var locationsResponse = _mediator.Send(new GetAllLocationsQuery()).Result;
            return MappingHelper.MapToLocationDtos(locationsResponse);
        }

        public IEnumerable<OfficeDto> GetOffices(string locationName)
        {
            var officesResponse = _mediator.Send(new GetAllOfficesQuery(locationName)).Result;
            return MappingHelper.MapToOfficeDtos(officesResponse);
        }

        public IEnumerable<OfficeDto> GetOfficeSuggestions(SuggestionsRequest request)
        {
            var query = new GetOfficeSuggestionsQuery(request.CapacityNeeded, request.PreferedNeigborHood, request.ResourcesNeeded);
            var suggestionsResponse = _mediator.Send(query).Result;

            return MappingHelper.MapToOfficeDtos(suggestionsResponse);
        }
    }
}