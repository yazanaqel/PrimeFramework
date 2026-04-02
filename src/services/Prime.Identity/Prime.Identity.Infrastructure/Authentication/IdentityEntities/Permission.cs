namespace Infrastructure.Authentication.IdentityEntities;
public class Permission
{
    public int Id { get; init; }
    public string PermissionName { get; init; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
    public ICollection<RolePermission>? RolePermissions { get; init; }
}
