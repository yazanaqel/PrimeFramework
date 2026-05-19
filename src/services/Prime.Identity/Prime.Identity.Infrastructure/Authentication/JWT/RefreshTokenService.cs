using Prime.Identity.Application.Abstractions.Auth;
using System.Security.Cryptography;

namespace Prime.Identity.Infrastructure.Authentication.JWT;

public class RefreshTokenService : IRefreshTokenService
{
    public string Generate()
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}
