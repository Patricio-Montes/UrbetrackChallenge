using MediatR;
using NetChallenge.Application.CQRS.Locations.Responses;
using System;

namespace NetChallenge.Application.CQRS.Locations.Read.GetById
{
    public record GetLocationByIdQuery(Guid Id) : IRequest<LocationResponse>;
}
