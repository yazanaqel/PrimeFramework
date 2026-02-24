using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.IdentityEntities;
public class Role : IdentityRole<Guid>
{
    public ICollection<UserRole>? UserRoles { get; set; }
    public ICollection<RolePermission>? RolePermissions { get; set; }
}
