using NetChallenge.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace NetChallenge.Domain
{
    public class Office : AggregateRoot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MaxCapacity { get; set; }
        public List<Resource> Resources { get; set; }
        public Location Local { get; set; }
    }
}