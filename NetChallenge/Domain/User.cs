using NetChallenge.Domain.ValueObjects;
using System;

namespace NetChallenge.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public PhoneNumber PhoneNumber { get; private set; }
    }
}
