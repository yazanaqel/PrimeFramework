namespace InsideMarket.MAUI.Components.Auth;

public interface ITokenStore
{
    Task SaveTokensAsync(TokenResponse tokenResponse);
    Task<TokenResponse?> GetTokensAsync();
    Task ClearTokensAsync();
}
