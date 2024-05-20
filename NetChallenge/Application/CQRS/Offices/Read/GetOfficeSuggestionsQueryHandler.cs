using MediatR;
using NetChallenge.Abstractions;
using NetChallenge.Application.CQRS.Offices.Read.CustomFilters;
using NetChallenge.Application.CQRS.Offices.Responses;
using NetChallenge.Domain;
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

        public GetOfficeSuggestionsQueryHandler(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository ?? throw new ArgumentNullException(nameof(officeRepository));
        }

        public async Task<IEnumerable<OfficeResponse>> Handle(GetOfficeSuggestionsQuery request, CancellationToken cancellationToken)
        {
            var specifications = new List<ISpecification<Office>>
            {
                OfficeSpecifications.CapacitySpecification(request.CapacityNeeded),
                OfficeSpecifications.ResourcesSpecification(request.ResourcesNeeded)
            };
            var compositeSpecification = OfficeSpecifications.CompositeSpecification(specifications);

            var filteredOffices = _officeRepository.AsEnumerable()
                                    .Where(compositeSpecification.IsSatisfiedBy);

            var orderingStrategies = new List<IOrderingStrategy<Office>>
            {
                OfficeOrderingStrategies.ExecuteStrategy(request.PreferedNeigborHood),
            };
            var compositeOrderingStrategy = OfficeOrderingStrategies.CompositeOrderingStrategy(orderingStrategies);

            var orderedOffices = compositeOrderingStrategy.Order(filteredOffices);

            return orderedOffices.Select(office => new OfficeResponse(
                office.Id,
                office.Location.Name,
                office.Name,
                office.MaxCapacity,
                office.AvailableResources
            ));
        }
    }
}
