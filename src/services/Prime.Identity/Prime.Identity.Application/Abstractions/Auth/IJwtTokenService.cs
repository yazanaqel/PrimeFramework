using Application.Features.User.RefreshToken;
using Prime.Identity.Domain.Entities.Users;

namespace Prime.Identity.Application.Abstractions.Auth;

public interface IJwtTokenService
{
    Task<TokenResponse> GenerateAccessTokenAsync(UserId userId,CancellationToken ct);
    Task<TokenResponse> RefreshTokenAsync(string accessToken,string refreshToken,CancellationToken ct);

}
