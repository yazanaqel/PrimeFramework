using Application.Abstractions.Messaging;

namespace Application.Features.User.RefreshToken;

public sealed record RefreshTokenCommand(RefreshTokenRequest Request,CancellationToken ct) : ICommand<TokenResponse?>;