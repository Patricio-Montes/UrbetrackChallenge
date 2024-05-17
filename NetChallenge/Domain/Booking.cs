using NetChallenge.Domain.Primitives;
using System;

namespace NetChallenge.Domain
{
    public class Booking : AggregateRoot
    {
        public Guid Id { get; set; }
        public Office Office { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public User User { get; set; }
    }
}