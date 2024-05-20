using System;

namespace NetChallenge.Domain
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Office Office { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public User User { get; set; }
    }
}