using Prime.Identity.Domain.Entities.Users;

namespace Prime.Identity.Application.Abstractions.Auth;

public interface IJwtTokenService
{
    Task<string> GenerateAccessToken(UserId userId);

}
