namespace InsideMarket.MAUI.Auth;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginRequest loginRequest);
    Task<bool> RegisterAsync(RegisterRequest registerRequest);
    Task LogoutAsync();
}
