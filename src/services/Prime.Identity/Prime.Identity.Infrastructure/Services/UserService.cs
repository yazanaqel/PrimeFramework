using Domain.Entities.Users;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Prime.Identity.Domain.Entities.Users;

namespace Prime.Services.Infrastructure.Services;

internal class UserService(
    UserManager<User> userManager,
    ApplicationDbContext applicationDbContext) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;



    public async Task<bool> RegisterAsync(AppUser appUser,CancellationToken ct)
    {
        var userExist = await _userManager.FindByEmailAsync(appUser.Email.Value);

        if(userExist is not null)
            return false;


        var strategy = _applicationDbContext.Database.CreateExecutionStrategy();

        User user = new User
        {
            Id = appUser.Id.Value,
            UserName = appUser.Email.Value,
            Email = appUser.Email.Value,
            PhoneNumber = appUser.PhoneNumber.Value,
        };

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _applicationDbContext.Database.BeginTransactionAsync(ct);

            await _userManager.CreateAsync(user,appUser.Password);

            if(appUser.Role.ToUpper() == nameof(Roles.MERCHANT))
            {
                await _userManager.AddToRoleAsync(user,nameof(Roles.MERCHANT));
            }
            else
            {
                await _userManager.AddToRoleAsync(user,nameof(Roles.USER));
            }

            await _applicationDbContext.SaveChangesAsync(ct);

            await transaction.CommitAsync(ct);
        });

        return true;
    }

    public async Task<AppUser> LoginAsync(string email,string password,CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is not null && await _userManager.CheckPasswordAsync(user,password))
        {
            UserId.TryParse(user.Id.ToString(),out UserId parsedUserId);

            return AppUser.AppUserResponse(parsedUserId,user.UserName);
        }

        return AppUser.EmptyAppUser();
    }



    public async Task<bool> LogoutAsync(UserId userId,CancellationToken ct)
    {
        string userIdString = userId.Value.ToString();
        var user = await _userManager.FindByIdAsync(userIdString);

        if(user is null)
            return false;

        user.RefreshToken = string.Empty;
        user.RefreshTokenExpiryTime = null;

        await _userManager.UpdateAsync(user);

        return true;
    }



}
