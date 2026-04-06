using Application.Abstractions.Messaging;
using CSharpFunctionalExtensions;
using Domain.Repositories;

namespace Application.Features.User.LogoutUser;

internal sealed class LogoutUserCommandHandler(IUserService userService) : ICommandHandler<LogoutUserCommand,bool>
{
    private readonly IUserService _userService = userService;

    public async Task<Result<bool>> Handle(LogoutUserCommand command,CancellationToken ct)
    {
        bool result = await _userService.LogoutAsync(command.UserId,ct);

        if(result)
            return Result.Success(result);

        return Result.Failure<bool>("Error");
    }

}