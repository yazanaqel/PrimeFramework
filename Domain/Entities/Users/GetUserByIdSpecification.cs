using Domain.Abstractions;

namespace Domain.Entities.Users;

public sealed class GetUserByIdSpecification : Specification<AppUser>
{
    public GetUserByIdSpecification(Guid userId)
    {
        Criteria = (u) => (u.Id == userId);
    }
}