using NetChallenge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetChallenge.Application.CQRS.Offices.Read.CustomFilters
{
    internal class OfficeOrderingStrategies
    {
        public static IOrderingStrategy<Office> NeighborhoodOrderingStrategy(string preferredNeighborhood)
        {
            return new OrderingStrategy<Office>(offices => offices.OrderByDescending(o => o.Location.Neighborhood == preferredNeighborhood));
        }

        public static IOrderingStrategy<Office> CapacityOrderingStrategy()
        {
            return new OrderingStrategy<Office>(offices => offices.ThenBy(o => o.MaxCapacity));
        }

        public static IOrderingStrategy<Office> ResourcesOrderingStrategy(IEnumerable<string> resourcesNeeded)
        {
            return new OrderingStrategy<Office>(offices =>
            {
                // Ordenar las oficinas por la cantidad de recursos necesarios
                return offices.OrderBy(o => o.Resources.Count(res => resourcesNeeded.Contains(res.Description)));
            });
        }

        public static IOrderingStrategy<Office> CompositeOrderingStrategy(IEnumerable<IOrderingStrategy<Office>> strategies)
        {
            return new CompositeOrderingStrategy<Office>(strategies);
        }
    }
}
