using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace InsideMarket.Web.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ITokenStore _tokenStore;

    public CustomAuthStateProvider(ITokenStore tokenStore)
    {
        _tokenStore = tokenStore;
    }


    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var accessToken = await _tokenStore.GetTokensAsync();

        if(accessToken is null || string.IsNullOrWhiteSpace(accessToken.AccessToken))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var claims = JwtParser.ParseClaims(accessToken.AccessToken);


        var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        await _tokenStore.SaveUserRoleAsync(new UserRole { Role = role,UserId = userId });

        var identity = new ClaimsIdentity(claims,"jwt");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}


public static class JwtParser
{
    public static IEnumerable<Claim> ParseClaims(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string,object>>(jsonBytes);

        var claims = new List<Claim>();

        foreach(var kvp in keyValuePairs)
        {
            // ROLE CLAIMS (all formats)
            if(kvp.Key == "role" ||
                kvp.Key == "roles" ||
                kvp.Key == ClaimTypes.Role ||
                kvp.Key == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
            {
                if(kvp.Value is JsonElement element && element.ValueKind == JsonValueKind.Array)
                {
                    foreach(var r in element.EnumerateArray())
                        claims.Add(new Claim(ClaimTypes.Role,r.GetString()!));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role,kvp.Value.ToString()!));
                }
            }

            // PERMISSIONS
            else if(kvp.Key == "Permissions")
            {
                if(kvp.Value is JsonElement element && element.ValueKind == JsonValueKind.Array)
                {
                    foreach(var p in element.EnumerateArray())
                        claims.Add(new Claim("permission",p.GetString()!));
                }
                else
                {
                    claims.Add(new Claim("permission",kvp.Value.ToString()!));
                }
            }

            // OTHER CLAIMS
            else
            {
                claims.Add(new Claim(kvp.Key,kvp.Value.ToString()!));
            }
        }

        return claims;
    }



    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        base64 = base64.Replace('-','+').Replace('_','/');

        switch(base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }


}

