using Domain.Primitives;

namespace Domain.Entities.Clients;
public class Client : IAuditableEntity
{
    public int ClientId { get; set; }
    public int AuthenticationMemberId { get; set; }
    public IAuthenticationMember AuthenticationMember { get; set; }

    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedOnUtc { get; set; }
}
