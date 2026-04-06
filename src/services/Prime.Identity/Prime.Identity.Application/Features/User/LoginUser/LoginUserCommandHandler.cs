using Application.Abstractions.Messaging;
using Application.Features.User.RefreshToken;
using CSharpFunctionalExtensions;
using Domain.Repositories;
using Prime.Identity.Application.Features.User.LoginUser;

namespace Application.Features.Authentication.LoginUser;

internal sealed class LoginUserCommandHandler(IUserService userService) : ICommandHandler<LoginUserCommand,LoginUserResponse>
{
    private readonly IUserService _userService = userService;
    public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand command,CancellationToken ct)
    {
        var user = await _userService.LoginAsync(command.Request.Email,command.Request.Password,ct);

        TokenResponse tokenResponse = new TokenResponse(user.AccessToken,user.RefreshToken);

        return Result.Success(new LoginUserResponse
        (
            user.Id,
            user.UserName,
            tokenResponse
        ));
    }

}