using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.IdentityEntities;

public class RolePermission : IdentityRoleClaim<Guid>
{
    public virtual Role? Role { get; set; }
    public virtual Permission? Permission { get; set; }
}
