using Application.Abstractions.Messaging;
using Prime.Identity.Domain.Entities.Users;

namespace Application.Features.User.LogoutUser;

public sealed record LogoutUserCommand(UserId UserId) : ICommand;
