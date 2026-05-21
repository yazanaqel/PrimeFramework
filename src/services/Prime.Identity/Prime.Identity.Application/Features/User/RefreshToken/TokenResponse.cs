namespace Application.Features.User.RefreshToken;

public record TokenResponse(string AccessToken,string RefreshToken,DateTime AccessTokenExpiresAt,DateTime RefreshTokenExpiresAt);
