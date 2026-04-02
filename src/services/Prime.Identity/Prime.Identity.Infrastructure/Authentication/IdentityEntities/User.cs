using Domain.Primitives;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.IdentityEntities;

public class User : IdentityUser<Guid>, IAuditableEntity
{
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }
    public ICollection<UserRole>? UserRoles { get; set; }
}
