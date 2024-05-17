using NetChallenge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetChallenge.Application.CQRS.Offices.Read.CustomFilters
{
    internal class OfficeSpecifications
    {
        public static ISpecification<Office> CapacitySpecification(int capacityNeeded)
        {
            return new Specification<Office>(o => o.MaxCapacity >= capacityNeeded);
        }

        public static ISpecification<Office> ResourcesSpecification(IEnumerable<string> resourcesNeeded)
        {
            return new Specification<Office>(o => !resourcesNeeded.Any() || resourcesNeeded.All(r => o.AvailableResources.Contains(r)));
        }

        public static ISpecification<Office> CompositeSpecification(IEnumerable<ISpecification<Office>> specifications)
        {
            return new Specification<Office>(o => specifications.All(s => s.IsSatisfiedBy(o)));
        }
    }
}
