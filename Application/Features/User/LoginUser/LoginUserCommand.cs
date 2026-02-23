using Application.Abstractions.Messaging;

namespace Application.Features.Authentication.LoginUser;
public sealed record LoginUserCommand(LoginUserRequest Request) : ICommand<string>;