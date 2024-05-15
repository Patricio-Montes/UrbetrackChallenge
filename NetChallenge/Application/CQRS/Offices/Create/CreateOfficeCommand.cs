using MediatR;
using System.Collections.Generic;

namespace NetChallenge.Application.CQRS.Offices.Create
{
    public record CreateOfficeCommand(
        string LocationName, 
        string Name, 
        int MaxCapacity, 
        IEnumerable<string> AvailableResources) : IRequest<Unit>;
}