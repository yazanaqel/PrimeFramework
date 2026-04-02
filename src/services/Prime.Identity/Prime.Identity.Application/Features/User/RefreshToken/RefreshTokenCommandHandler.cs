using Application.Abstractions.Messaging;
using Application.Repositories;
using CSharpFunctionalExtensions;

namespace Application.Features.User.RefreshToken;

internal sealed class RefreshTokenCommandHandler(IUserIdentity userIdentity) : ICommandHandler<RefreshTokenCommand,RefreshTokenResponse?>
{
    private readonly IUserIdentity _userIdentity = userIdentity;

    public async Task<Result<RefreshTokenResponse?>> Handle(RefreshTokenCommand command,CancellationToken cancellationToken)
        => Result.Success(await _userIdentity.RefreshTokenAsync(command.Request.AccessToken,command.Request.RefreshToken));

}