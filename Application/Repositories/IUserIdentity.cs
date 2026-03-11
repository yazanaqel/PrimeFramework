using Domain.Abstractions;
using Domain.Entities.Users;

namespace Application.Repositories;

public interface IUserIdentity
{
    Task<string> RegisterAsync(AppUser appUser,CancellationToken cancellationToken);
    Task<string> LoginAsync(string email,string password);
    Task<bool> IsEmailAvailable(string email,CancellationToken cancellationToken);
    Task<IEnumerable<AppUser>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task<AppUser> GetAsync(ISpecification<AppUser> spec,CancellationToken cancellationToken = default);
}