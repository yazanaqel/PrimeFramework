using Ardalis.Specification;
using Prime.Identity.Queries.Domain.Entities.Business;

namespace Prime.Identity.Queries.Domain.Specifications.Business;

public sealed class GetOwnerStoreByIdSpec : SingleResultSpecification<Store>
{
    public GetOwnerStoreByIdSpec(Guid userId)
    {
        Query.Where((u) => (u.UserId == userId));
    }
}
