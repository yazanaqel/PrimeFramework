using System.Text.Json.Serialization;

namespace InsideMarket.MAUI.Auth;

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsStoreAccount { get; set; } = false;
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
