using Ardalis.Specification;
using Prime.Identity.Queries.Domain.Entities.Business;

namespace Prime.Identity.Queries.Domain.Specifications.Business;


public sealed class GetProductByIdSpec : Specification<Product>
{
    public GetProductByIdSpec(Guid productId)
    {
        Query.Where(p => p.Id == productId);

        Query.Include(p => p.Store);

        Query.Include(p => p.Category);

        Query.AsNoTracking();
    }
}
