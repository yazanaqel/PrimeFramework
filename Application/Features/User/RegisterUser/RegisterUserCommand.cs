using Application.Abstractions.Messaging;

namespace Application.Features.Authentication.RegisterUser;
public record RegisterUserCommand(RegisterUserRequest Request) : ICommand<string>;