using MediatR;
using NetChallenge.Application.CQRS.Locations.Responses;
using System.Collections.Generic;

namespace NetChallenge.Application.CQRS.Locations.Read.GetAll
{
    public record GetAllLocationsQuery() : IRequest<IEnumerable<LocationResponse>>;
}
