using Application.Abstractions.Messaging;
using Application.Repositories;
using CSharpFunctionalExtensions;

namespace Application.Features.Authentication.LoginUser;

internal sealed class LoginUserCommandHandler(IUserIdentity userIdentity) : ICommandHandler<LoginUserCommand,string>
{
    private readonly IUserIdentity _userIdentity = userIdentity;

    public async Task<Result<string>> Handle(LoginUserCommand command,CancellationToken cancellationToken)
    {
        var result = await _userIdentity.LoginAsync(command.Request.Email,command.Request.Password);

        if(string.IsNullOrWhiteSpace(result))
        {
            return Result.Failure<string>("Failed to login user.");
        }

        return Result.Success(result);
    }
}