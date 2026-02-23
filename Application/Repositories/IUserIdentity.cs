namespace Application.Repositories;

public interface IUserIdentity
{
    Task<string> RegisterAsync(string email,string password);
    Task<string> LoginAsync(string email,string password);
}