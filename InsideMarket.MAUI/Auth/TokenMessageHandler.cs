using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace InsideMarket.MAUI.Auth;

public class TokenMessageHandler : DelegatingHandler
{
    private readonly ITokenStore _tokenStore;
    private readonly IServiceProvider _serviceProvider;

    //private readonly HttpClient _http;

    public TokenMessageHandler(ITokenStore tokenStore,IServiceProvider serviceProvider)
    {
        _tokenStore = tokenStore;
        _serviceProvider = serviceProvider;
        //_http = httpClientFactory.CreateClient("ApiClient");
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var tokensResponse = await _tokenStore.GetTokensAsync();

        if(!string.IsNullOrWhiteSpace(tokensResponse?.AccessToken) && tokensResponse.AccessTokenExpiresAt > DateTime.UtcNow)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",tokensResponse.AccessToken);

        var response = await base.SendAsync(request,cancellationToken);

        if(response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var refreshed = await HandleRefreshAsync();

            if(!refreshed)
                return response;

            var newTokens = await _tokenStore.GetTokensAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",newTokens?.AccessToken);

            return await base.SendAsync(request,cancellationToken);
        }

        return response;
    }


    private async Task<bool> HandleRefreshAsync()
    {
        var oldTokens = await _tokenStore.GetTokensAsync();

        if(string.IsNullOrEmpty(oldTokens?.RefreshToken))
            return false;

        var refreshRequest = new RefreshTokenRequest
        {
            AccessToken = oldTokens?.AccessToken ?? string.Empty,
            RefreshToken = oldTokens?.RefreshToken ?? string.Empty
        };

        var factory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
        var http = factory.CreateClient("ApiClient");

        var response = await http.PostAsJsonAsync("Users/Refresh",refreshRequest);

        if(!response.IsSuccessStatusCode)
            return false;

        var newTokens = await response.Content.ReadFromJsonAsync<TokenResponse>();

        if(newTokens is null)
            return false;

        await _tokenStore.SaveTokensAsync(newTokens);

        return true;
    }

}
