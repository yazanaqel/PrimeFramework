using Infrastructure.Authentication.IdentityEntities;

namespace Infrastructure.Authentication;
public interface IJwtProvider
{
    Task<string> GenerateAsync(User member);
}
