using Application.Abstractions.Messaging;
using CSharpFunctionalExtensions;
using Domain.Repositories;

namespace Application.Features.User.RefreshToken;

internal sealed class RefreshTokenCommandHandler(IUserService userService) : ICommandHandler<RefreshTokenCommand,TokenResponse?>
{
    private readonly IUserService _userService = userService;

    public async Task<Result<TokenResponse?>> Handle(RefreshTokenCommand command,CancellationToken ct)
    {
        var result = await _userService.RefreshTokenAsync(command.Request.AccessToken,command.Request.RefreshToken,ct);

        if (string.IsNullOrEmpty(result.RefreshToken))
        {
            return Result.Failure<TokenResponse?>("Invalid token");
        }

        return new TokenResponse(result.AccessToken,result.RefreshToken);
    }

}