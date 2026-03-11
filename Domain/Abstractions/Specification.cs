using System.Linq.Expressions;

namespace Domain.Abstractions;

public abstract class Specification<T> : ISpecification<T>
{
    public Expression<Func<T,bool>>? Criteria { get; protected set; }

    public List<Expression<Func<T,object>>> IncludesInternal { get; } = new();
    public IReadOnlyList<Expression<Func<T,object>>> Includes => IncludesInternal;

    public Expression<Func<T,object>>? OrderBy { get; protected set; }
    public Expression<Func<T,object>>? OrderByDescending { get; protected set; }

    public int? Skip { get; protected set; }
    public int? Take { get; protected set; }
    public bool IsPagingEnabled => Skip.HasValue || Take.HasValue;

    protected void AddInclude(Expression<Func<T,object>> include) =>
        IncludesInternal.Add(include);

    protected void ApplyOrderBy(Expression<Func<T,object>> orderBy) =>
        OrderBy = orderBy;

    protected void ApplyOrderByDescending(Expression<Func<T,object>> orderByDesc) =>
        OrderByDescending = orderByDesc;

    protected void ApplyPaging(int skip,int take)
    {
        Skip = skip;
        Take = take;
    }
}
