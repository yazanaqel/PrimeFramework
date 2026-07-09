namespace InsideMarket.MAUI.Auth;

public interface ITokenStore
{
    Task SaveTokensAsync(TokenResponse tokenResponse);
    Task SaveRoleAsync(string role);
    Task<TokenResponse?> GetTokensAsync();
    Task ClearTokensAsync();
}
