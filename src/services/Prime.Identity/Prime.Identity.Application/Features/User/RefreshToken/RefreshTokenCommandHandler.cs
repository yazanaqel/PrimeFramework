using Application.Abstractions.Messaging;
using CSharpFunctionalExtensions;
using Prime.Identity.Application.Abstractions.Auth;

namespace Application.Features.User.RefreshToken;

internal sealed class RefreshTokenCommandHandler(IJwtTokenService jwtTokenService) : ICommandHandler<RefreshTokenCommand,TokenResponse?>
{
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

    public async Task<Result<TokenResponse?>> Handle(RefreshTokenCommand command,CancellationToken ct)
    {
        var result = await _jwtTokenService.RefreshTokenAsync(command.Request.AccessToken,command.Request.RefreshToken,ct);

        if(string.IsNullOrEmpty(result.RefreshToken))
        {
            return Result.Failure<TokenResponse?>("Invalid token");
        }

        return Result.Success<TokenResponse?>(result);
    }

}