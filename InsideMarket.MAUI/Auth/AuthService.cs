using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace InsideMarket.MAUI.Auth;


public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly ITokenStore _tokenStore;
    private readonly CustomAuthStateProvider _authStateProvider;

    public AuthService(
        IHttpClientFactory httpClientFactory,
        ITokenStore tokenStore,
        AuthenticationStateProvider authStateProvider)
    {
        _http = httpClientFactory.CreateClient("ApiClient");
        _tokenStore = tokenStore;
        _authStateProvider = (CustomAuthStateProvider)authStateProvider;
    }

    public async Task<bool> LoginAsync(LoginRequest loginRequest)
    {

        var response = await _http.PostAsJsonAsync("Users/Login",loginRequest);

        if(!response.IsSuccessStatusCode)
            return false;

        var result = await response.Content.ReadFromJsonAsync<LoginResult>();
        if(result?.TokenResponse is null)
            return false;

        var tokenResponse = result.TokenResponse;

        await _tokenStore.SaveTokensAsync(tokenResponse);

        await _authStateProvider.MarkUserAsAuthenticated(tokenResponse.AccessToken);
        return true;
    }

    public async Task<bool> RegisterAsync(RegisterRequest registerRequest)
    {
        if (registerRequest.IsStoreAccount)
        {
            registerRequest.Role = "Merchant";
        }

        var response = await _http.PostAsJsonAsync("Users/Register",registerRequest);

        if(!response.IsSuccessStatusCode)
            return false;

        return true;
    }

    public async Task LogoutAsync()
    {
        // 1. Get the current user ID from stored claims
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var userId = authState.User.FindFirst("sub")?.Value;

        if(!string.IsNullOrWhiteSpace(userId))
        {
            // 2. Call your API logout endpoint
            await _http.PostAsync($"Users/Logout/{userId}",null);
        }

        // 3. Clear tokens from SecureStorage
        await _tokenStore.ClearTokensAsync();

        // 4. Notify the UI that the user is logged out
        _authStateProvider.MarkUserAsLoggedOut();
    }

}
