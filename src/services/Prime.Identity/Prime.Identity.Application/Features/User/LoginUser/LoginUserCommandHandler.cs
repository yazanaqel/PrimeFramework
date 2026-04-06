using Application.Abstractions.Messaging;
using Application.Features.User.RefreshToken;
using CSharpFunctionalExtensions;
using Domain.Repositories;

namespace Application.Features.Authentication.LoginUser;

internal sealed class LoginUserCommandHandler(IUserService userService) : ICommandHandler<LoginUserCommand,RefreshTokenResponse?>
{
    private readonly IUserService _userService = userService;
    public async Task<Result<RefreshTokenResponse?>> Handle(LoginUserCommand command,CancellationToken ct)
        => Result.Success(await _userService.LoginAsync(command.Request.Email,command.Request.Password,ct));

}