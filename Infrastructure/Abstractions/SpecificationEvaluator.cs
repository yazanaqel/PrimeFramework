using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions;

public static class SpecificationEvaluator
{
    public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery,ISpecification<T> spec)
        where T : class
    {
        var query = inputQuery;

        // Apply filter
        if(spec.Criteria is not null)
            query = query.Where(spec.Criteria);

        return query;
    }
}
