namespace Infrastructure.Authentication;
public interface IPermissionService
{
    Task<UserAccessInfo> GetUserAccessInfoAsync(Guid userId);

}
