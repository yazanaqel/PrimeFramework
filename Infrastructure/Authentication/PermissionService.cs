using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication;

public class PermissionService(ApplicationDbContext dbContext) : IPermissionService
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Dictionary<string,HashSet<string>>> GetRolePermissionsAsync(int userId)
    {
        var rolePermissions = await _dbContext.Set<IdentityEntities.User>()
            .Where(u => u.Id == userId)
            .SelectMany(u => u.UserRoles)
            .GroupBy(ur => ur.Role.Name)
            .Select(g => new
            {
                RoleName = g.Key,
                Permissions = g.SelectMany(ur => ur.Role.RolePermissions)
                               .Select(rp => rp.Permission.PermissionName)
                               .ToHashSet()
            })
            .ToDictionaryAsync(x => x.RoleName,x => x.Permissions);

        return rolePermissions;
    }

}
