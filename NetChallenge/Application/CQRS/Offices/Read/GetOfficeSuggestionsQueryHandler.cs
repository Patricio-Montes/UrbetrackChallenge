using MediatR;
using NetChallenge.Abstractions;
using NetChallenge.Application.CQRS.Offices.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetChallenge.Application.CQRS.Offices.Read
{
    public sealed class GetOfficeSuggestionsQueryHandler : IRequestHandler<GetOfficeSuggestionsQuery, IEnumerable<OfficeResponse>>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly ILocationRepository _locationRepository;

        public GetOfficeSuggestionsQueryHandler(IOfficeRepository officeRepository, ILocationRepository locationRepository)
        {
            _officeRepository = officeRepository ?? throw new ArgumentNullException(nameof(officeRepository));
            _locationRepository = locationRepository ?? throw new ArgumentException(nameof(locationRepository));
        }

        public async Task<IEnumerable<OfficeResponse>> Handle(GetOfficeSuggestionsQuery request, CancellationToken cancellationToken)
        {
            var allOffices = await _officeRepository.GetAllAsync();

            var filteredOffices = allOffices
                .Where(o => o.MaxCapacity >= request.CapacityNeeded)
                .Where(o => string.IsNullOrEmpty(request.PreferedNeigborHood) || o.Location.Neighborhood == request.PreferedNeigborHood)
                .Where(o => !request.ResourcesNeeded.Any() || o.Resources.Select(r => r.Description).Intersect(request.ResourcesNeeded).Any())
                .ToList();

            return filteredOffices.Select(office => new OfficeResponse(
                office.Id,
                office.Location.Name,
                office.Name,
                office.MaxCapacity,
                office.AvailableResources
            ));
        }
    }
}
