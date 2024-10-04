using Domain.Shared;

namespace Application.Abstractions; 

public interface IUserRepository
{
    Task<Result<string>> RegisterAsync(string email, string password);
    Task<Result<string>> LoginAsync(string email, string password);
}