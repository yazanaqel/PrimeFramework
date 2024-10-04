using Application.Abstractions.Messaging;

namespace Application.Features.Authentication.LoginUser;
public record LoginUserCommand(LoginUserRequest Request) : ICommand<string>;