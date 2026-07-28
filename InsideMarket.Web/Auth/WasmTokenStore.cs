using Microsoft.JSInterop;

namespace InsideMarket.Web.Auth;

public class WasmTokenStore : ITokenStore
{
    private readonly IJSRuntime _js;

    private const string AccessTokenKey = "access_token";
    private const string RefreshTokenKey = "refresh_token";
    private const string AccessTokenExpiresAt = "access_expires";
    private const string RefreshTokenExpiresAt = "refresh_expires";
    private const string RoleKey = "role";
    private const string UserIdKey = "userId";

    public WasmTokenStore(IJSRuntime js)
    {
        _js = js;
    }

    public async Task SaveTokensAsync(TokenResponse tokenResponse)
    {
        await _js.InvokeVoidAsync("localStorage.setItem",AccessTokenKey,tokenResponse.AccessToken);
        await _js.InvokeVoidAsync("localStorage.setItem",RefreshTokenKey,tokenResponse.RefreshToken);
        await _js.InvokeVoidAsync("localStorage.setItem",AccessTokenExpiresAt,tokenResponse.AccessTokenExpiresAt);
        await _js.InvokeVoidAsync("localStorage.setItem",RefreshTokenExpiresAt,tokenResponse.RefreshTokenExpiresAt);

    }

    public async Task SaveUserRoleAsync(UserRole userRole)
    {
        await _js.InvokeVoidAsync("localStorage.setItem",RoleKey,userRole.Role);
        await _js.InvokeVoidAsync("localStorage.setItem",UserIdKey,userRole.UserId);

    }


    public async Task ClearTokensAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem",AccessTokenKey);
        await _js.InvokeVoidAsync("localStorage.removeItem",RefreshTokenKey);
        await _js.InvokeVoidAsync("localStorage.removeItem",AccessTokenExpiresAt);
        await _js.InvokeVoidAsync("localStorage.removeItem",RefreshTokenExpiresAt);
        await _js.InvokeVoidAsync("localStorage.removeItem",RoleKey);
        await _js.InvokeVoidAsync("localStorage.removeItem",UserIdKey);
    }


    public async Task<TokenResponse?> GetTokensAsync()
    {
        var access = await _js.InvokeAsync<string?>("localStorage.getItem",AccessTokenKey);
        var refresh = await _js.InvokeAsync<string?>("localStorage.getItem",RefreshTokenKey);
        var accessExpiresStr = await _js.InvokeAsync<string?>("localStorage.getItem",AccessTokenExpiresAt);
        var refreshExpiresStr = await _js.InvokeAsync<string?>("localStorage.getItem",RefreshTokenExpiresAt);


        DateTime accessExpires = DateTime.UtcNow;
        if(!string.IsNullOrEmpty(accessExpiresStr) && DateTime.TryParse(accessExpiresStr,out var dt))
            accessExpires = dt;

        DateTime refreshExpires = DateTime.UtcNow;
        if(!string.IsNullOrEmpty(refreshExpiresStr) && DateTime.TryParse(refreshExpiresStr,out var dt2))
            refreshExpires = dt2;

        return new TokenResponse
        {
            AccessToken = access ?? "",
            RefreshToken = refresh ?? "",
            AccessTokenExpiresAt = accessExpires,
            RefreshTokenExpiresAt = refreshExpires
        };
    }

}
