using System.Collections.Generic;
using System.Linq;

namespace NetChallenge.Application.CQRS
{
    public class CompositeOrderingStrategy<T> : IOrderingStrategy<T>
    {
        private readonly IEnumerable<IOrderingStrategy<T>> _strategies;

        public CompositeOrderingStrategy(IEnumerable<IOrderingStrategy<T>> strategies)
        {
            _strategies = strategies;
        }

        public IOrderedEnumerable<T> Order(IEnumerable<T> collection)
        {
            IOrderedEnumerable<T> orderedCollection = collection.OrderBy(e => 0);

            foreach (var strategy in _strategies)
            {
                orderedCollection = strategy.Order(orderedCollection);
            }

            return orderedCollection;
        }
    }
}
