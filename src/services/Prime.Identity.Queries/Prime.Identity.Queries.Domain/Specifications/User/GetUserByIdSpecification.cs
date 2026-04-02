using Ardalis.Specification;
using Domain.Entities.User;

namespace Domain.Specifications.User;

public sealed class GetUserByIdSpecification : SingleResultSpecification<AppUser>
{
    public GetUserByIdSpecification(Guid userId)
    {
        Query.Where((u) => (u.Id == userId));
    }
}