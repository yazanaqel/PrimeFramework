using Application.Abstractions.Messaging;
using Application.Repositories;
using CSharpFunctionalExtensions;

namespace Application.Features.User.LogoutUser;

internal sealed class LogoutUserCommandHandler(IUserIdentity userIdentity) : ICommandHandler<LogoutUserCommand>
{
    private readonly IUserIdentity _userIdentity = userIdentity;

    public async Task<Result> Handle(LogoutUserCommand command,CancellationToken cancellationToken)
    {
        await _userIdentity.LogoutAsync(command.UserId);

        return Result.Success();
    }

}