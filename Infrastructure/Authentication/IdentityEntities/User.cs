using Domain.Primitives;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.IdentityEntities;

public class User : IdentityUser<Guid>, IAuditableEntity
{
    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedOnUtc { get; set; }
    public ICollection<UserRole>? UserRoles { get; set; }
}
