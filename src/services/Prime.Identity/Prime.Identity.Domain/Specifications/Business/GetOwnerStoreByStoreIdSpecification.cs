using Ardalis.Specification;
using Prime.Identity.Domain.Entities.Stores;

namespace Prime.Identity.Domain.Specifications.Business;

public sealed class GetOwnerStoreByStoreIdSpecification : SingleResultSpecification<Store>
{
    public GetOwnerStoreByStoreIdSpecification(StoreId storeId)
    {
        Query.Where((u) => (u.Id == storeId)).AsNoTracking();
    }
}
