using Domain.Shared;

namespace Application.Repositories; 

public interface IUserRepository
{
    Task<Result<string>> RegisterAsync(string email, string password);
    Task<Result<string>> LoginAsync(string email, string password);
}