using Application.Repositories;
using Domain.Errors;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

internal class UserIdentity(UserManager<User> userManager,IJwtProvider jwtProvider) : IUserIdentity
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;


    public async Task<string> LoginAsync(string email,string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is not null && await _userManager.CheckPasswordAsync(user,password))
        {
            string token = await _jwtProvider.GenerateAsync(user);

            return token;
        }

        return DomainErrors.User.InvalidCredentials;
    }

    public async Task<string> RegisterAsync(string email,string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is not null)
            return DomainErrors.User.EmailAlreadyInUse;


        var registerUser = new User { UserName = email,Email = email };

        var result = await _userManager.CreateAsync(registerUser,password);

        if(result.Succeeded)
            await userManager.AddToRoleAsync(registerUser,nameof(Roles.USER));

        string token = await _jwtProvider.GenerateAsync(registerUser);

        return token;

    }
}
