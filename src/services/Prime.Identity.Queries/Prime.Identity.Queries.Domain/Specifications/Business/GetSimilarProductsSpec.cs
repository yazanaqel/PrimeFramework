using Ardalis.Specification;
using Prime.Identity.Queries.Domain.Entities.Business;

namespace Prime.Identity.Queries.Domain.Specifications.Business;

public sealed class GetSimilarProductsSpec : Specification<Product>
{
    public GetSimilarProductsSpec(Guid categoryId, Guid productId)
    {
        Query.Where(p => p.CategoryId == categoryId && p.Id != productId);

        Query.Include(p => p.Store);

        Query.AsNoTracking();
    }
}
