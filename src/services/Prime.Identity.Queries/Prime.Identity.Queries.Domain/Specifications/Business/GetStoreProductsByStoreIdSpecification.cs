using Ardalis.Specification;
using Prime.Identity.Queries.Domain.Entities.Business;

namespace Prime.Identity.Queries.Domain.Specifications.Business;

public sealed class GetStoreProductsByStoreIdSpecification : Specification<Product>
{
    public GetStoreProductsByStoreIdSpecification(Guid storeId)
    {
        Query.Where((u) => (u.Store.Id == storeId));
    }
}
