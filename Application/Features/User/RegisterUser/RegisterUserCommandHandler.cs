using Application.Abstractions.Messaging;
using Application.Repositories;
using CSharpFunctionalExtensions;
using Domain.Entities.Users;

namespace Application.Features.Authentication.RegisterUser;

internal sealed class RegisterUserCommandHandler(IUserIdentity userIdentity) : ICommandHandler<RegisterUserCommand,string>
{
    private readonly IUserIdentity _userIdentity = userIdentity;

    public async Task<Result<string>> Handle(RegisterUserCommand command,CancellationToken cancellationToken)
    {
        var availableEmail = await _userIdentity.IsEmailAvailable(command.Request.Email);

        if(!availableEmail)
            return Result.Failure<string>("Email Is Not Available");

        var user = await _userIdentity.RegisterAsync(
            appUser: new AppUser(command.Request.Email,command.Request.Email,command.Request.Password));

        return Result.Success(user);
    }
}
