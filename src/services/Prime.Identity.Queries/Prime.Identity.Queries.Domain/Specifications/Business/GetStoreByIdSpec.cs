using Ardalis.Specification;
using Prime.Identity.Queries.Domain.Entities.Business;

namespace Prime.Identity.Queries.Domain.Specifications.Business;

public sealed class GetStoreByIdSpec : Specification<Store>
{
    public GetStoreByIdSpec(Guid storeId)
    {
        Query.Where((u) => (u.Id == storeId));
    }
}
