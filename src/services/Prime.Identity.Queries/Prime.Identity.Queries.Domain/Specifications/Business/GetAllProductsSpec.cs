using Ardalis.Specification;
using Prime.Identity.Queries.Domain.Entities.Business;
using Prime.Identity.Queries.Domain.Entities.Enums;

namespace Prime.Identity.Queries.Domain.Specifications.Business;

public sealed class GetAllProductsSpec : Specification<Product>
{
    public GetAllProductsSpec()
    {

        Query.Include(s => s.Store);

        Query.Where(s => s.Store.StoreStatus == StoreStatus.Active);

        Query.Include(c => c.Category).ThenInclude(p => p.SubCategories);

    }
}
