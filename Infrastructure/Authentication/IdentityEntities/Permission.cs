using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.IdentityEntities;
public class Permission
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public ICollection<RolePermission>? RolePermissions { get; init; }
}
