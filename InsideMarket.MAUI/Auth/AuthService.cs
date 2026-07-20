using InsideMarket.MAUI.Business.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using static InsideMarket.MAUI.Route.RouteGate;

namespace InsideMarket.MAUI.Auth;


public class AuthService : IAuthService
{
    private readonly HttpClient _httpRead;
    private readonly HttpClient _httpWrite;
    private readonly ITokenStore _tokenStore;
    private readonly IBasketService _basketService;
    private readonly CustomAuthStateProvider _authStateProvider;

    public AuthService(
        IHttpClientFactory httpClientFactory,
        ITokenStore tokenStore,
        AuthenticationStateProvider authStateProvider,
        IBasketService basketService)
    {
        _httpRead = httpClientFactory.CreateClient("Read");
        _httpWrite = httpClientFactory.CreateClient("Write");
        _tokenStore = tokenStore;
        _basketService = basketService;
        _authStateProvider = (CustomAuthStateProvider)authStateProvider;
    }

    public async Task<bool> LoginAsync(LoginRequest loginRequest)
    {

        var response = await _httpWrite.PostAsJsonAsync(UsersRouteGate.Login,loginRequest);

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

        var response = await _httpWrite.PostAsJsonAsync(UsersRouteGate.Register,registerRequest);

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
            await _httpWrite.PostAsync($"{UsersRouteGate.Logout}/{userId}",null);
        }

        // 3. Clear the basket
        await _basketService.ClearBasket();

        // 4. Clear tokens from SecureStorage
        await _tokenStore.ClearTokensAsync();

        // 5. Notify the UI that the user is logged out
        _authStateProvider.MarkUserAsLoggedOut();
    }

    public async Task<UserProfileInfo> GetUserProfileAsync()
    {
        var response = await _httpRead.GetFromJsonAsync<UserProfileInfo>(UsersRouteGate.GetUserProfile);
        return response ?? new UserProfileInfo();

    }
}
