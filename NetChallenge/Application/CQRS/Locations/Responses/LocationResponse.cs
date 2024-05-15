using System;

namespace NetChallenge.Application.CQRS.Locations.Responses
{
    public record LocationResponse(
        Guid Id,
        string Name,
        string Neighborhood);
}
