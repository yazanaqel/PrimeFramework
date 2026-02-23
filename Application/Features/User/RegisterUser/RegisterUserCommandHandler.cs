using Application.Abstractions.Messaging;
using Application.Repositories;
using CSharpFunctionalExtensions;

namespace Application.Features.Authentication.RegisterUser;

internal sealed class RegisterUserCommandHandler(IUserIdentity userIdentity) : ICommandHandler<RegisterUserCommand,string>
{
    private readonly IUserIdentity _userIdentity = userIdentity;

    public async Task<Result<string>> Handle(RegisterUserCommand command,CancellationToken cancellationToken)
    {
        var result = await _userIdentity.RegisterAsync(command.Request.Email,command.Request.Password);

        if(string.IsNullOrWhiteSpace(result))
        {
            return Result.Failure<string>("Failed to register user.");
        }

        return Result.Success(result);
    }
}
