namespace InsideMarket.MAUI.Components.Auth;

public class PreferencesTokenStore : ITokenStore
{
    private const string AccessTokenKey = "access_token";
    private const string RefreshTokenKey = "refresh_token";
    private const string AccessTokenExpiresAt = "access_token_expires_at";
    private const string RefreshTokenExpiresAt = "refresh_token_expires_at";

    public Task SaveTokensAsync(TokenResponse tokenResponse)
    {
        Preferences.Set(AccessTokenKey, tokenResponse.AccessToken);
        Preferences.Set(RefreshTokenKey, tokenResponse.RefreshToken);
        Preferences.Set(AccessTokenExpiresAt, tokenResponse.AccessTokenExpiresAt.ToString("O"));
        Preferences.Set(RefreshTokenExpiresAt, tokenResponse.RefreshTokenExpiresAt.ToString("O"));
        return Task.CompletedTask;
    }

    public Task<TokenResponse?> GetTokensAsync()
    {
        var access = Preferences.Get(AccessTokenKey,null);
        var refresh = Preferences.Get(RefreshTokenKey,null);
        var accessExpiresStr = Preferences.Get(AccessTokenExpiresAt,null);
        var refreshExpiresStr = Preferences.Get(RefreshTokenExpiresAt,null);

        DateTime accessExpires = DateTime.UtcNow;
        if(!string.IsNullOrEmpty(accessExpiresStr) && DateTime.TryParse(accessExpiresStr,out var dt))
            accessExpires = dt;

        DateTime refreshExpires = DateTime.UtcNow;
        if(!string.IsNullOrEmpty(refreshExpiresStr) && DateTime.TryParse(refreshExpiresStr,out var dt2))
            refreshExpires = dt2;

        return Task.FromResult(new TokenResponse
        {
            AccessToken = access,
            RefreshToken = refresh,
            AccessTokenExpiresAt = accessExpires,
            RefreshTokenExpiresAt = refreshExpires
        });
    }

    public Task ClearTokensAsync()
    {
        Preferences.Remove(AccessTokenKey);
        Preferences.Remove(RefreshTokenKey);
        Preferences.Remove(AccessTokenExpiresAt);
        Preferences.Remove(RefreshTokenExpiresAt);
        return Task.CompletedTask;
    }

}
