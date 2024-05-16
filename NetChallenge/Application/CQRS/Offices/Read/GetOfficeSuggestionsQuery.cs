using MediatR;
using NetChallenge.Application.CQRS.Offices.Responses;
using System.Collections.Generic;

namespace NetChallenge.Application.CQRS.Offices.Read
{
    public record GetOfficeSuggestionsQuery(
        int CapacityNeeded,
        string PreferedNeigborHood,
        IEnumerable<string> ResourcesNeeded) : IRequest<IEnumerable<OfficeResponse>>;
}