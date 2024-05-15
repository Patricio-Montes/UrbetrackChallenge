using System.Collections.Generic;

namespace NetChallenge.Domain.Primitives
{
    public abstract class AggregateRoot
    {
        private readonly List<DomainEvent> domainEvents = new List<DomainEvent>();
        public ICollection<DomainEvent> GetDomainEvents() => domainEvents;
        protected void Raise(DomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }
    }
}
