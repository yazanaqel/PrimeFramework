using Application.Abstractions.Messaging;
using Application.Repositories;
using CSharpFunctionalExtensions;
using Domain.Entities.Users;
using Domain.ValueObjects;

namespace Application.Features.Authentication.RegisterUser;

internal sealed class RegisterUserCommandHandler(IUserIdentity userIdentity) : ICommandHandler<RegisterUserCommand,string>
{
    private readonly IUserIdentity _userIdentity = userIdentity;

    public async Task<Result<string>> Handle(RegisterUserCommand command,CancellationToken cancellationToken)
    {

        if(Email.TryCreate(command.Request.Email,out var email))
        {
            var availableEmail = await _userIdentity.IsEmailAvailable(email.Value);

            if(!availableEmail)
                return Result.Failure<string>("Email Is Not Available");
        }
        else
        {
            return Result.Failure<string>("Email Is Not Valid");
        }

        var user = await _userIdentity.RegisterAsync(
            appUser: new AppUser(email,command.Request.Email,command.Request.Password));

        return Result.Success(user);
    }
}
