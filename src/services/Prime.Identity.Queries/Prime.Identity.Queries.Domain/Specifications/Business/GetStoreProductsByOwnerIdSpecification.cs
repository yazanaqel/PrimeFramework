using Ardalis.Specification;
using Prime.Identity.Queries.Domain.Entities.Business;
namespace Prime.Identity.Queries.Domain.Specifications.Business;

public sealed class GetStoreProductsByOwnerIdSpecification : Specification<Product>
{
    public GetStoreProductsByOwnerIdSpecification(Guid userId)
    {
        Query.Where((u) => (u.Store.UserId == userId));
    }
}
