using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions;

public static class SpecificationEvaluator
{
    public static IQueryable<T> GetQuery<T>(
        IQueryable<T> inputQuery,
        ISpecification<T> spec)
        where T : class
    {
        var query = inputQuery;

        // Apply filter
        if(spec.Criteria is not null)
            query = query.Where(spec.Criteria);

        // Apply includes
        foreach(var include in spec.Includes)
            query = query.Include(include);

        // Apply ordering
        if(spec.OrderBy is not null)
            query = query.OrderBy(spec.OrderBy);
        else if(spec.OrderByDescending is not null)
            query = query.OrderByDescending(spec.OrderByDescending);

        // Apply paging
        if(spec.IsPagingEnabled)
        {
            if(spec.Skip.HasValue)
                query = query.Skip(spec.Skip.Value);

            if(spec.Take.HasValue)
                query = query.Take(spec.Take.Value);
        }

        return query;
    }
}
