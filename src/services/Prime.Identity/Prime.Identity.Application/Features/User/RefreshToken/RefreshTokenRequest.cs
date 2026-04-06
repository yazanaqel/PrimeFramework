namespace Application.Features.User.RefreshToken;

public sealed record RefreshTokenRequest(string AccessToken,string RefreshToken);