using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NetChallenge.Application.CQRS.Locations.Create;
using NetChallenge.Application.CQRS.Locations.Read.GetAll;
using NetChallenge.Application.Services;
using NetChallenge.Dto.Input;
using NetChallenge.Dto.Output;

namespace NetChallenge
{
    public class OfficeRentalService : IOfficeRentalService
    {
        private readonly IMediator _mediator;

        public OfficeRentalService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task AddLocation(AddLocationRequest request)
        {
            try
            {
                var command = MapToAddLocationCommand(request);
                await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private CreateLocationCommand MapToAddLocationCommand(AddLocationRequest request)
        {
            return new CreateLocationCommand(request.Name, request.Neighborhood);
        }

        public void AddOffice(AddOfficeRequest request)
        {
            throw new NotImplementedException();
        }

        public void BookOffice(BookOfficeRequest request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookingDto> GetBookings(string locationName, string officeName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LocationDto> GetLocations()
        {
            var locationsResponse = _mediator.Send(new GetAllLocationsQuery()).Result;

            var locationDtos = locationsResponse.Select(locationResponse => new LocationDto
            {
                Name = locationResponse.Name,
                Neighborhood = locationResponse.Neighborhood
            });

            return locationDtos;
        }

        public IEnumerable<OfficeDto> GetOffices(string locationName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OfficeDto> GetOfficeSuggestions(SuggestionsRequest request)
        {
            throw new NotImplementedException();
        }

        void IOfficeRentalService.AddLocation(AddLocationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}