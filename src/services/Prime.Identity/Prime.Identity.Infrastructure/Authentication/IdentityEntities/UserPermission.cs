using Infrastructure.Authentication.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace Prime.Identity.Infrastructure.Authentication.IdentityEntities;

public class UserPermission : IdentityUserClaim<Guid>
{
    public virtual User? User { get; set; }
}
