using NetChallenge.Domain.ValueObjects;
using System;

namespace NetChallenge.Domain
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Neighborhood { get; set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Address Address { get; private set; } = Address.Empty;
    }
}