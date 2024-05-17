using System.Collections.Generic;
using System.Linq;
using System;

namespace NetChallenge.Application.CQRS
{
    public class OrderingStrategy<T> : IOrderingStrategy<T>
    {
        private readonly Func<IOrderedEnumerable<T>, IOrderedEnumerable<T>> _orderFunc;

        public OrderingStrategy(Func<IOrderedEnumerable<T>, IOrderedEnumerable<T>> orderFunc)
        {
            _orderFunc = orderFunc;
        }

        public IOrderedEnumerable<T> Order(IOrderedEnumerable<T> collection)
        {
            return _orderFunc(collection);
        }

        public IOrderedEnumerable<T> Order(IEnumerable<T> collection)
        {
            return _orderFunc(collection.OrderBy(e => 0));
        }
    }
}
