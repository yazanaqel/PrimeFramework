using Domain.Entities.Users;
using Prime.Identity.Domain.Entities.Users;

namespace Domain.Repositories;

public interface IUserService
{
    Task<bool> RegisterAsync(AppUser appUser,CancellationToken ct);
    Task<AppUser> LoginAsync(string email,string password,CancellationToken ct);
    Task<bool> LogoutAsync(UserId userId,CancellationToken ct);
}