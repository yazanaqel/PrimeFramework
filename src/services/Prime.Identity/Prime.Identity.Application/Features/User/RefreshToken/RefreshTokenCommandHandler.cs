using Application.Abstractions.Messaging;
using CSharpFunctionalExtensions;
using Domain.Repositories;

namespace Application.Features.User.RefreshToken;

internal sealed class RefreshTokenCommandHandler(IUserService userService) : ICommandHandler<RefreshTokenCommand,RefreshTokenResponse?>
{
    private readonly IUserService _userService = userService;

    public async Task<Result<RefreshTokenResponse?>> Handle(RefreshTokenCommand command,CancellationToken ct)
        => Result.Success(await _userService.RefreshTokenAsync(command.Request.AccessToken,command.Request.RefreshToken,ct));

}