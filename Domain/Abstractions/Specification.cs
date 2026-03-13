using System.Linq.Expressions;

namespace Domain.Abstractions;

public abstract class Specification<T> : ISpecification<T>
{
    public Expression<Func<T,bool>>? Criteria { get; protected set; }

}
