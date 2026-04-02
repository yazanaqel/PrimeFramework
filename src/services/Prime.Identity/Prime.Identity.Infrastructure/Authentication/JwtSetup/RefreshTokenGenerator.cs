using System.Security.Cryptography;

namespace Infrastructure.Authentication.JwtSetup;

public class RefreshTokenGenerator
{
    public string Generate()
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}
