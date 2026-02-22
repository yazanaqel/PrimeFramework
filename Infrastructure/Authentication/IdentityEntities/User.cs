using Domain.Primitives;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.IdentityEntities;

public class User : IdentityUser<int>, IAuditableEntity
{
    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedOnUtc { get; set; }
    public ICollection<UserRole>? UserRoles { get; set; }
}
