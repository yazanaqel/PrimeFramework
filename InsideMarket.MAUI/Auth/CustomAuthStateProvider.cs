using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InsideMarket.MAUI.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ITokenStore _tokenStore;

    public CustomAuthStateProvider(ITokenStore tokenStore)
    {
        _tokenStore = tokenStore;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        TokenResponse? tokenResponse = await _tokenStore.GetTokensAsync();

        if(string.IsNullOrWhiteSpace(tokenResponse?.AccessToken) || tokenResponse.AccessTokenExpiresAt <= DateTime.UtcNow)
        {
            // Not authenticated
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(anonymous);
        }

        var identity = CreateIdentityFromJwt(tokenResponse.AccessToken);
        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

    public async Task MarkUserAsAuthenticated(string accessToken)
    {
        var identity = CreateIdentityFromJwt(accessToken);
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void MarkUserAsLoggedOut()
    {
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }

    private ClaimsIdentity CreateIdentityFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();

        var token = handler.ReadJwtToken(jwt);

        var role = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        var userId = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        _tokenStore.SaveUserRoleAsync(new UserRole { Role = role, UserId = userId });

        var claims = token.Claims;

        return new ClaimsIdentity(claims,"jwt");
    }
}
