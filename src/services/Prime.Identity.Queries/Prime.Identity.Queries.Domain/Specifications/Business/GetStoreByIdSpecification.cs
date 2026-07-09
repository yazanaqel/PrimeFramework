using Ardalis.Specification;
using Prime.Identity.Queries.Domain.Entities.Business;

namespace Prime.Identity.Queries.Domain.Specifications.Business;

public sealed class GetStoreByIdSpecification : Specification<Store>
{
    public GetStoreByIdSpecification(Guid storeId)
    {
        Query.Where((u) => (u.Id == storeId));
    }
}
