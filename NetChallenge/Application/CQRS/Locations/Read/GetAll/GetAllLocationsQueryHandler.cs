using MediatR;
using NetChallenge.Abstractions;
using NetChallenge.Application.CQRS.Locations.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetChallenge.Application.CQRS.Locations.Read.GetAll
{
    public sealed class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery, IEnumerable<LocationResponse>>
    {
        private readonly ILocationRepository _locationRepository;

        public GetAllLocationsQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        }

        public async Task<IEnumerable<LocationResponse>> Handle(GetAllLocationsQuery query, CancellationToken cancellationToken)
        {
            var locations = await _locationRepository.GetAllAsync();

            return locations
                   .Select(location => new LocationResponse(
                   location.Id,
                   location.Name,
                   location.Neighborhood))
                   .ToList();
        }
    }
}
