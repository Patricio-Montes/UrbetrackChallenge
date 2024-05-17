using System.Collections.Generic;
using System.Linq;

namespace NetChallenge.Application.CQRS
{
    public interface IOrderingStrategy<T>
    {
        IOrderedEnumerable<T> Order(IEnumerable<T> collection);
    }
}
