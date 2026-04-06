using Application.Abstractions.Messaging;
using CSharpFunctionalExtensions;
using Domain.Repositories;

namespace Application.Features.User.LogoutUser;

internal sealed class LogoutUserCommandHandler(IUserService userService) : ICommandHandler<LogoutUserCommand>
{
    private readonly IUserService _userService = userService;

    public async Task<Result> Handle(LogoutUserCommand command,CancellationToken ct)
    {
        await _userService.LogoutAsync(command.UserId,ct);

        return Result.Success();
    }

}