using Application.Features.User.RefreshToken;
using Domain.Abstractions;
using Domain.Entities.Users;

namespace Application.Repositories;

public interface IUserIdentity
{
    Task<RefreshTokenResponse?> RegisterAsync(AppUser appUser,CancellationToken cancellationToken);
    Task<RefreshTokenResponse?> LoginAsync(string email,string password);
    Task<RefreshTokenResponse?> RefreshTokenAsync(string accessToken,string refreshToken);
    Task LogoutAsync(Guid userId);
    Task<bool> IsEmailAvailable(string email,CancellationToken cancellationToken);
    Task<IEnumerable<AppUser>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task<AppUser> GetAsync(ISpecification<AppUser> spec,CancellationToken cancellationToken = default);
}