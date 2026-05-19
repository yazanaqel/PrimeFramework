using Prime.Identity.Domain.Entities.Users;

namespace Infrastructure.Authentication;
public interface IPermissionService
{
    Task<UserAccessInfo> GetUserAccessInfoAsync(UserId userId);

}
