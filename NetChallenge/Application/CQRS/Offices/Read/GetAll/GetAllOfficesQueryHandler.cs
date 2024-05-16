using MediatR;
using NetChallenge.Abstractions;
using NetChallenge.Application.CQRS.Offices.Responses;
using NetChallenge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetChallenge.Application.CQRS.Offices.Read.GetAll
{
    public sealed class GetAllOfficesQueryHandler : IRequestHandler<GetAllOfficesQuery, IEnumerable<OfficeResponse>>
    {
        private readonly IOfficeRepository _officeRepository;

        public GetAllOfficesQueryHandler(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository ?? throw new ArgumentException(nameof(officeRepository));
        }

        public async Task<IEnumerable<OfficeResponse>> Handle(GetAllOfficesQuery request, CancellationToken cancellationToken)
        {
            var offices = _officeRepository.Get(request.locationName);

            if (offices is null || !offices.Any())
            {
                return Enumerable.Empty<OfficeResponse>();
            }

            return offices.Select(office => MapToOfficeResponse(office));
        }

        private OfficeResponse MapToOfficeResponse(Office office)
        {
            return new OfficeResponse(
                office.Id,
                office.Location.Name,
                office.Name,
                office.MaxCapacity,
                office.AvailableResources
           );
        }
    }
}
