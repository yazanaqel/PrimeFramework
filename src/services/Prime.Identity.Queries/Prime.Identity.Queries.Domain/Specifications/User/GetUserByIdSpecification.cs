using Ardalis.Specification;
using Domain.Entities.User;
using Prime.Identity.Queries.Domain.Entities.Business;

namespace Domain.Specifications.User;

public sealed class GetUserByIdSpecification : SingleResultSpecification<AppUser>
{
    public GetUserByIdSpecification(Guid userId)
    {
        Query.Where((u) => (u.Id == userId));
    }
}



