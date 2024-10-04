using Infrastructure.Authentication;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories.Authentication;
internal class UserRepository(ApplicationDbContext dbContext, UserManager<User> userManager, IJwtProvider jwtProvider) : IUserRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<bool> RegisterAsync(User member)
    {
        var isEmailUnique = await _userManager.FindByEmailAsync(member.Email);

        if (isEmailUnique is not null)
            return false;

        var result = await _userManager.CreateAsync(member, "Qwerty1@345");

        _dbContext.SaveChanges();

        if (result.Succeeded)
            return true;

        return false;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var member = await _userManager.FindByEmailAsync(email);

        if (member is null)
            return string.Empty;

        string token = await _jwtProvider.GenerateAsync(member);
        return token;
    }
}
