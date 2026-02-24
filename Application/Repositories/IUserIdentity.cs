using Domain.Entities.Users;

namespace Application.Repositories;

public interface IUserIdentity
{
    Task<string> RegisterAsync(AppUser appUser);
    Task<string> LoginAsync(string email,string password);
    Task<bool> IsEmailAvailable(string email);
    Task<IEnumerable<AppUser>> GetAllUsersAsync();
}