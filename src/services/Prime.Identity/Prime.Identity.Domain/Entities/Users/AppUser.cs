using Domain.Primitives;
using Domain.ValueObjects;
using Prime.Identity.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Users;

public class AppUser : Entity<UserId>
{
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; } = string.Empty;
    public string Role { get; private set; }
    public string AccessToken { get; private set; } = string.Empty;
    public string RefreshToken { get; private set; } = string.Empty;

    private AppUser() { }
    public AppUser(UserId userId,Email email,string userName,string password,string role,PhoneNumber phoneNumber)
    {
        Id = userId;
        Email = email;
        UserName = userName;
        Password = password;
        Role = role;
        PhoneNumber = phoneNumber;

        AddDomainEvent(new UserRegisteredEvent(Guid.NewGuid(),Id,email.Value,DateTime.UtcNow));
    }

    public static AppUser EmptyAppUser()
    {
        return new AppUser();
    }

    public static AppUser AppUserResponse(UserId userId,string userName)
    {
        return new AppUser
        {
            Id = userId,
            UserName = userName
        };

    }
}