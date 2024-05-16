using MediatR;
using NetChallenge.Application.CQRS.Offices.Responses;
using System.Collections.Generic;

namespace NetChallenge.Application.CQRS.Offices.Read.GetAll
{
    public record GetAllOfficesQuery(string locationName) : IRequest<IEnumerable<OfficeResponse>>;
}
