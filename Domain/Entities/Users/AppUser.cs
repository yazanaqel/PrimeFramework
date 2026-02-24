namespace Domain.Entities.Users;

public class AppUser : Primitives.Entity<Guid>
{
    public string Email { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; } = string.Empty;
    private AppUser() { }
    public AppUser(string email,string userName,string password)
    {
        Id = Guid.NewGuid();
        Email = email;
        UserName = userName;
        Password = password;

        AddDomainEvent(new UserRegisteredEvent(Guid.NewGuid(),Id,email,DateTime.UtcNow));
    }
    public static AppUser MapUser(Guid userId,string email,string userName)
    {
        return new AppUser
        {
            Id = userId,
            Email = email,
            UserName = userName
        };
    }
}