namespace Infrastructure.Authentication.JwtSetup;
public class JwtOptions
{
    public string Issuer { get; init; } = string.Empty;

    public string Audience { get; init; } = string.Empty;

    public string SecretKey { get; init; } = string.Empty;
    public int AccessTokenMinutes { get; set; }
    public int RefreshTokenDays { get; set; }
}