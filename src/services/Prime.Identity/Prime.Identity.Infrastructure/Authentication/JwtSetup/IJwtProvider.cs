using Infrastructure.Authentication.IdentityEntities;

namespace Infrastructure.Authentication.JwtSetup;
public interface IJwtProvider
{
    Task<string> GenerateAccessToken(User user);
}
