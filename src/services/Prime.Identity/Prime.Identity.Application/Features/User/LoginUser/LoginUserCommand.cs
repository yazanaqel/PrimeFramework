using Application.Abstractions.Messaging;
using Prime.Identity.Application.Features.User.LoginUser;

namespace Application.Features.Authentication.LoginUser;

public sealed record LoginUserCommand(LoginUserRequest Request,CancellationToken ct) : ICommand<LoginUserResponse>;