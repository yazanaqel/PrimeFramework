using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.IdentityEntities;
public class UserRole : IdentityUserRole<int>
{
    public virtual User? User { get; set; }
    public virtual Role? Role { get; set; }
}
