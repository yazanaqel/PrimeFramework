using Application.Abstractions.Messaging;
using Application.Features.User.RefreshToken;

namespace Application.Features.Authentication.LoginUser;
public sealed record LoginUserCommand(LoginUserRequest Request,CancellationToken cancellationToken) : ICommand<RefreshTokenResponse?>;