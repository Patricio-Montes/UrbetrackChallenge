using System;
using System.Collections.Generic;

namespace NetChallenge.Application.CQRS.Offices.Responses
{
    public record OfficeResponse(
        Guid Id,
        string LocationName,
        string Name,
        int MaxCapacity,
        IEnumerable<string> AvailableResources);
}
