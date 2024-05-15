using System;

namespace NetChallenge.Domain
{
    public class Resource
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
    }
}
