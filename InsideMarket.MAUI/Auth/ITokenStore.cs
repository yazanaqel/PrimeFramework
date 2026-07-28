using Microsoft.JSInterop;


namespace InsideMarket.MAUI.Auth;

public interface ITokenStore
{
    Task SaveTokensAsync(TokenResponse tokenResponse);
    Task SaveUserRoleAsync(UserRole userRole);
    Task<TokenResponse?> GetTokensAsync();
    Task ClearTokensAsync();
}


