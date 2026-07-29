using System.Net.Http.Json;

namespace InsideMarket.Web.Auth;

public class AuthService : BaseApiClient, IAuthService
{

    public AuthService(HttpClient http,ITokenStore tokenStore,CustomAuthStateProvider authProvider) : base(http,tokenStore,authProvider)
    {
    }

    private const string _read = "https://localhost:7125/";

    public async Task<bool> LoginAsync(LoginRequest loginRequest)
    {

        var response = await _http.PostAsJsonAsync("https://localhost:7104/Users/Login",loginRequest);

        if(!response.IsSuccessStatusCode)
            return false;

        var auth = await response.Content.ReadFromJsonAsync<LoginResult>();

        if(auth is not null && auth.TokenResponse is not null)
        {
            await _tokenStore.SaveTokensAsync(auth.TokenResponse);

            _authProvider.NotifyAuthenticationStateChanged();

            return true;
        }

        return false;
    }

    public async Task<IEnumerable<Store>> GetAllStores()
    {
        var response = await SendWithRefreshAsync(() =>
    _http.GetAsync("https://localhost:7125/api/Stores/GetAllStores")
);
        if(!response.IsSuccessStatusCode)
            return new List<Store>();

        return await response.Content.ReadFromJsonAsync<IEnumerable<Store>>()
               ?? new List<Store>();

    }

    public async Task<Store> GetStoreById(Guid storeId)
    {

        var response = await SendWithRefreshAsync(() =>
    _http.GetAsync($"https://localhost:7125/api/Stores/GetStoreById/{storeId}")
);
        if(!response.IsSuccessStatusCode)
            return new Store();

        return await response.Content.ReadFromJsonAsync<Store>()
               ?? new Store();

    }

    public async Task<UserProfileInfo> GetUserProfileAsync()
    {
        var response = await SendWithRefreshAsync(() =>
            _http.GetAsync("https://localhost:7125/api/Home/GetUserProfile")
        );

        if(!response.IsSuccessStatusCode)
            return new UserProfileInfo();

        return await response.Content.ReadFromJsonAsync<UserProfileInfo>()
               ?? new UserProfileInfo();
    }

    public async Task LogoutAsync()
    {
        // 1. Get the current user ID from stored claims
        var authState = await _authProvider.GetAuthenticationStateAsync();
        var userId = authState.User.FindFirst("sub")?.Value;

        if(!string.IsNullOrWhiteSpace(userId))
        {
            // 2. Call your API logout endpoint
            await _http.PostAsync($"https://localhost:7104/Users/{userId}",null);
        }

        // 3. Clear tokens from SecureStorage
        await _tokenStore.ClearTokensAsync();

        // 4. Notify the UI that the user is logged out
        _authProvider.NotifyAuthenticationStateChanged();
    }

    protected override async Task<bool> RefreshTokenAsync()
    {
        var tokenResponse = await _tokenStore.GetTokensAsync();

        if(tokenResponse is null || string.IsNullOrWhiteSpace(tokenResponse.AccessToken) || string.IsNullOrWhiteSpace(tokenResponse.RefreshToken))
            return false;


        var response = await _http.PostAsJsonAsync("https://localhost:7104/Users/Refresh",new RefreshTokenRequest
        {
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken

        });

        if(!response.IsSuccessStatusCode)
            return false;

        var auth = await response.Content.ReadFromJsonAsync<TokenResponse>();

        if(auth is null)
            return false;

        await _tokenStore.SaveTokensAsync(auth);

        return true;
    }

    public async Task<CursorPageResponse<GetAllUsersResponse>> GetAllUsersAsync()
    {
        var response = await SendWithRefreshAsync(() =>
            _http.GetAsync("https://localhost:7125/api/Home/GetAllUsers")
        );

        if(!response.IsSuccessStatusCode)
            return new CursorPageResponse<GetAllUsersResponse>();

        return await response.Content.ReadFromJsonAsync<CursorPageResponse<GetAllUsersResponse>>()
               ?? new CursorPageResponse<GetAllUsersResponse>();
    }

    public async Task<GetUserByIdResponse> GetUserByIdAsync(string userId)
    {
        var response = await SendWithRefreshAsync(() =>
            _http.GetAsync($"https://localhost:7125/api/Home/GetUserById/{userId}")
        );

        if(!response.IsSuccessStatusCode)
            return new GetUserByIdResponse();

        return await response.Content.ReadFromJsonAsync<GetUserByIdResponse>()
               ?? new GetUserByIdResponse();
    }

    public async Task<bool> ChangeStoreStatus(StoreChangeStatus storeChangeStatus)
    {
        var response = await _http.PostAsJsonAsync("https://localhost:7104/api/Stores/ChangeStoreStatus",storeChangeStatus);

        if(!response.IsSuccessStatusCode)
            return false;

        return true;

    }
}

