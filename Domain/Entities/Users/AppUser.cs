using Domain.ValueObjects;

namespace Domain.Entities.Users;

public class AppUser : Primitives.Entity<Guid>
{
    public Email Email { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; } = string.Empty;
    private AppUser() { }
    public AppUser(Email email,string userName,string password)
    {
        Id = Guid.NewGuid();
        Email = email;
        UserName = userName;
        Password = password;

        AddDomainEvent(new UserRegisteredEvent(Guid.NewGuid(),Id,email.Value,DateTime.UtcNow));
    }
}