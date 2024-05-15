using MediatR;
using System;

namespace NetChallenge.Domain.Primitives
{
    public record DomainEvent(Guid Id) : INotification
    {
    }
}
