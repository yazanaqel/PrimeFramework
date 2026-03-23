using Application.Abstractions.Messaging;

namespace Application.Features.User.LogoutUser;

public sealed record LogoutUserCommand(Guid UserId) : ICommand;
