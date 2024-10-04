using Domain.Entities.Clients;
using Domain.Primitives;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.IdentityEntities;
public class User : IdentityUser<int>, IAuditableEntity, IAuthenticationUser
{
    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedOnUtc { get; set; }
    public virtual Client? Client { get; set; }
    public int? ClientId { get; set; }
    public ICollection<UserRole>? UserRoles { get; set; }
}
