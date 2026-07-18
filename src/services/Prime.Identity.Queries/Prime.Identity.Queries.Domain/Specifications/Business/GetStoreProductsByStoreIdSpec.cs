using Ardalis.Specification;
using Prime.Identity.Queries.Domain.Entities.Business;

namespace Prime.Identity.Queries.Domain.Specifications.Business;

public sealed class GetStoreProductsByStoreIdSpec : Specification<Product>
{
    public GetStoreProductsByStoreIdSpec(Guid storeId)
    {
        Query.Where((u) => (u.Store.Id == storeId));
    }
}
