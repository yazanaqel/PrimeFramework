using Domain.Primitives;
using Microsoft.AspNetCore.Identity;
using Prime.Identity.Infrastructure.Authentication.IdentityEntities;

namespace Infrastructure.Authentication.IdentityEntities;

public class User : IdentityUser<Guid>, IAuditableEntity
{
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }
    public ICollection<UserRole>? UserRoles { get; set; }
    public ICollection<UserPermission>? UserPermissions { get; set; }
}
