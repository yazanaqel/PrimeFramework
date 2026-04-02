using Application.Abstractions.Messaging;
using Application.Features.User.RefreshToken;

namespace Application.Features.Authentication.RegisterUser;
public sealed record RegisterUserCommand(RegisterUserRequest Request,CancellationToken cancellationToken) : ICommand<RefreshTokenResponse?>;