using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InsideMarket.Web.Auth;

public interface ITokenStore
{
    Task SaveTokensAsync(TokenResponse tokenResponse);
    Task SaveUserRoleAsync(UserRole userRole);
    Task<TokenResponse?> GetTokensAsync();
    Task ClearTokensAsync();
}
public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
    public bool Remember { get; set; }
}
public class TokenResponse
{
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; } = string.Empty;

    [JsonPropertyName("accessTokenExpiresAt")]
    public DateTime AccessTokenExpiresAt { get; set; }

    [JsonPropertyName("RefreshTokenExpiresAt")]
    public DateTime RefreshTokenExpiresAt { get; set; }
}
public class UserRole
{
    public string Role { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}
public class LoginResult
{
    [JsonPropertyName("userId")]
    public UserIdWrapper? UserId { get; set; }

    [JsonPropertyName("userName")]
    public string? UserName { get; set; }

    [JsonPropertyName("tokenResponse")]
    public TokenResponse? TokenResponse { get; set; }
}

public class UserIdWrapper
{
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}

public class RefreshTokenRequest
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
public class UserProfileInfo
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}
