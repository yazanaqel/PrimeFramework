using Application.Abstractions.Messaging;

namespace Application.Features.Authentication.RegisterUser;
public sealed record RegisterUserCommand(RegisterUserRequest Request,CancellationToken cancellationToken) : ICommand<string>;