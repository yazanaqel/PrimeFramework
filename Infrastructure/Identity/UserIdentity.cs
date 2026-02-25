using Application.Features.User.GetAllUsers;
using Application.Repositories;
using Domain.Entities.Users;
using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Infrastructure.Authentication.JwtSetup;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

internal class UserIdentity(UserManager<User> userManager,IJwtProvider jwtProvider,ApplicationDbContext applicationDbContext) : IUserIdentity
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

    public async Task<bool> IsEmailAvailable(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is not null)
            return false;

        return true;
    }

    public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
    {
        return await _userManager.Users
            .Select(u => AppUser.MapUser
            (
                u.Id,
                u.Email,
                u.UserName
            )).ToListAsync();
    }

    public async Task<string> LoginAsync(string email,string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is not null && await _userManager.CheckPasswordAsync(user,password))
            return await _jwtProvider.GenerateAsync(user);

        return "Wrong Email Or Password";
    }

    public async Task<string> RegisterAsync(AppUser appUser)
    {
        var strategy = _applicationDbContext.Database.CreateExecutionStrategy();

        string token = string.Empty;

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            var user = new User { Id = appUser.Id,UserName = appUser.Email.Value,Email = appUser.Email.Value };

            await _userManager.CreateAsync(user,appUser.Password);

            await userManager.AddToRoleAsync(user,nameof(Roles.USER));

            token = await _jwtProvider.GenerateAsync(user);

            _applicationDbContext.Set<AppUser>().Add(appUser);

            await _applicationDbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        });

        return token;
    }

}
