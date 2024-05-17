namespace NetChallenge.Application.CQRS
{
    internal interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
    }
}
