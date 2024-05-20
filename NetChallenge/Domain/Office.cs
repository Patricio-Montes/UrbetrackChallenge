using System;
using System.Collections.Generic;
using System.Linq;

namespace NetChallenge.Domain
{
    public class Office
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MaxCapacity { get; set; }
        public List<Resource> Resources { get; set; }
        public Location Location { get; set; }

        public IEnumerable<string> AvailableResources => Resources?.Where(r => r.Available)?.Select(r => r.Description) ?? Enumerable.Empty<string>();
    }
}