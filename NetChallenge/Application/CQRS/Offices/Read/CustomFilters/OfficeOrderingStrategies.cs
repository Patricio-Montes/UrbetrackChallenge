using NetChallenge.Domain;
using System.Collections.Generic;
using System.Linq;

namespace NetChallenge.Application.CQRS.Offices.Read.CustomFilters
{
    internal class OfficeOrderingStrategies
    {
        public static IOrderingStrategy<Office> ExecuteStrategy(string preferredNeighborhood, IEnumerable<string> resourcesNeeded)
        {
            return new OrderingStrategy<Office>(offices =>
                offices.OrderByDescending(o => o.Location.Neighborhood.Equals(preferredNeighborhood))
                .ThenBy(o => o.MaxCapacity)
                .ThenBy(o => o.AvailableResources.Count()));
        }

        public static IOrderingStrategy<Office> CompositeOrderingStrategy(IEnumerable<IOrderingStrategy<Office>> strategies)
        {
            return new CompositeOrderingStrategy<Office>(strategies);
        }
    }
}
