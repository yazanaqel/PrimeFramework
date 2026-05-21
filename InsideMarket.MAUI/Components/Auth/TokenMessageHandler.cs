using System.Net.Http.Headers;

namespace InsideMarket.MAUI.Components.Auth;

public class TokenMessageHandler : DelegatingHandler
{
    private readonly ITokenStore _tokenStore;

    public TokenMessageHandler(ITokenStore tokenStore)
    {
        _tokenStore = tokenStore;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
    {
        TokenResponse? tokenResponse = await _tokenStore.GetTokensAsync();

        if(!string.IsNullOrWhiteSpace(tokenResponse?.AccessToken) && tokenResponse.AccessTokenExpiresAt > DateTime.UtcNow)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",tokenResponse.AccessToken);
        }

        return await base.SendAsync(request,cancellationToken);
    }
}
