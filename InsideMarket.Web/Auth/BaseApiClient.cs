using System.Net;
using System.Net.Http.Headers;

namespace InsideMarket.Web.Auth;

public abstract class BaseApiClient
{
    protected readonly HttpClient _http;
    protected readonly ITokenStore _tokenStore;
    protected readonly CustomAuthStateProvider _authProvider;

    protected BaseApiClient(
        HttpClient http,
        ITokenStore tokenStore,
        CustomAuthStateProvider authProvider)
    {
        _http = http;
        _tokenStore = tokenStore;
        _authProvider = authProvider;
    }

    protected async Task AttachTokenAsync()
    {
        var token = await _tokenStore.GetTokensAsync();

        _http.DefaultRequestHeaders.Authorization = null;

        if(token is not null && !string.IsNullOrWhiteSpace(token.AccessToken))
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer",token.AccessToken);
        }
    }

    protected async Task<HttpResponseMessage> SendWithRefreshAsync(
        Func<Task<HttpResponseMessage>> send)
    {
        await AttachTokenAsync();

        var response = await send();

        if(response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var refreshed = await RefreshTokenAsync();

            if(!refreshed)
            {
                // refresh failed → user is logged out
                await _tokenStore.ClearTokensAsync();
                _authProvider.NotifyAuthenticationStateChanged();
                return response;
            }

            // refresh succeeded → notify UI
            _authProvider.NotifyAuthenticationStateChanged();

            await AttachTokenAsync();
            return await send();
        }

        return response;
    }

    protected abstract Task<bool> RefreshTokenAsync();
}
