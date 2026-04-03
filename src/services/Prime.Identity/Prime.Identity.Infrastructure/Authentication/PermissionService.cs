using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication;

public class PermissionService(ApplicationDbContext dbContext) : IPermissionService
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    //public async Task<Dictionary<string,HashSet<string>>> GetRolePermissionsAsync(Guid userId)
    //{
    //    var rolePermissions = await _dbContext.Set<IdentityEntities.User>()
    //        .Where(u => u.Id == userId)
    //        .SelectMany(u => u.UserRoles)
    //        .GroupBy(ur => ur.Role.Name)
    //        .Select(g => new
    //        {
    //            RoleName = g.Key,
    //            Permissions = g.SelectMany(ur => ur.Role.RolePermissions)
    //                           .Select(rp => rp.Permission.PermissionName)
    //                           .ToHashSet()
    //        })
    //        .ToDictionaryAsync(x => x.RoleName,x => x.Permissions);

    //    return rolePermissions;
    //}

    public async Task<UserAccessInfo> GetUserAccessInfoAsync(Guid userId)
    {
        return await _dbContext.Users
            .Where(u => u.Id == userId)
            .Select(u => new UserAccessInfo(
                u.UserPermissions.Select(c => c.ClaimValue).ToList(),
                u.UserRoles.Select(r => r.Role.Name).ToList()
            )).FirstAsync();
    }

}
public record UserAccessInfo(
    IReadOnlyList<string> Permissions,
    IReadOnlyList<string> Roles);