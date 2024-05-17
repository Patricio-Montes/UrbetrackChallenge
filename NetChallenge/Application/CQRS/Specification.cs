using System;

namespace NetChallenge.Application.CQRS
{
    public class Specification<T> : ISpecification<T>
    {
        private readonly Func<T, bool> _expression;

        public Specification(Func<T, bool> expression)
        {
            _expression = expression;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return _expression(entity);
        }
    }
}
