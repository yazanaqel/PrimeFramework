namespace Infrastructure.Authentication;
public interface IPermissionService
{
    Task<Dictionary<string, HashSet<string>>> GetRolePermissionsAsync(int userId);
}
