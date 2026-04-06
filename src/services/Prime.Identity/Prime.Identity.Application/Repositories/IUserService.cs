using Application.Features.User.RefreshToken;
using Domain.Entities.Users;
using Prime.Identity.Domain.Entities.Users;

namespace Domain.Repositories;

public interface IUserService
{
    Task<RefreshTokenResponse?> RegisterAsync(AppUser appUser,CancellationToken ct);
    Task<RefreshTokenResponse?> LoginAsync(string email,string password,CancellationToken ct);
    Task<RefreshTokenResponse?> RefreshTokenAsync(string accessToken,string refreshToken,CancellationToken ct);
    Task LogoutAsync(UserId userId,CancellationToken ct);
    Task<bool> IsEmailAvailable(string email,CancellationToken ct);
}