namespace InsideMarket.MAUI.Components.Auth;

public interface IAuthService
{
    Task<bool> LoginAsync(string username,string password);
    Task LogoutAsync();
}
