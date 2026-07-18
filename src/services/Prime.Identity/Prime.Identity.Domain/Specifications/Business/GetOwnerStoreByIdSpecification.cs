using Ardalis.Specification;
using Prime.Identity.Domain.Entities.Products;
using Prime.Identity.Domain.Entities.Stores;
using Prime.Identity.Domain.Entities.Users;

namespace Prime.Identity.Domain.Specifications.Business;

public sealed class GetOwnerStoreByIdSpecification : SingleResultSpecification<Store>
{
    public GetOwnerStoreByIdSpecification(UserId userId)
    {
        Query.Where((u) => (u.UserId == userId));
    }
}

public class ProductsByIdsSpec : Specification<Product>
{
    public ProductsByIdsSpec(IEnumerable<ProductId> productIds)
    {
        Query.Where(p => productIds.Contains(p.Id));
        Query.Include(p => p.Store);
    }
}
