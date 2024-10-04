using Infrastructure.Authentication.IdentityEntities;

namespace Infrastructure.Repositories.Authentication;
public interface IUserRepository
{
    Task<bool> RegisterAsync(User user);
    Task<string> LoginAsync(string email, string password);
}