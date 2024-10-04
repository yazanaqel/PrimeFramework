using Application.Abstractions;
using Domain.Errors;
using Domain.Shared;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories.Authentication;
internal class UserRepository(UserManager<User> userManager, IJwtProvider jwtProvider) : IUserRepository
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;


    public async Task<Result<string>> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is not null && await _userManager.CheckPasswordAsync(user, password))
        {
            string token = await _jwtProvider.GenerateAsync(user);

            return Result.Success(token);
        }

        return Result.Failure<string>(DomainErrors.User.InvalidCredentials);
    }

    public async Task<Result<string>> RegisterAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is not null)
            return Result.Failure<string>(DomainErrors.User.EmailAlreadyInUse);


        var registerUser = new User { UserName = email, Email = email };

        var result = await _userManager.CreateAsync(registerUser, password);

        if (result.Succeeded)
            await userManager.AddToRoleAsync(registerUser, nameof(Roles.USER));

        string token = await _jwtProvider.GenerateAsync(registerUser);

        return Result.Success(token);

    }
}
