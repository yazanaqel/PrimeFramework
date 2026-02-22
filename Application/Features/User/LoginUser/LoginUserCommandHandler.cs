using Application.Abstractions.Messaging;
using Application.Repositories;
using Domain.Shared;

namespace Application.Features.Authentication.LoginUser;

internal sealed class LoginUserCommandHandler(IUserIdentity userIdentity) : ICommandHandler<LoginUserCommand, string>
{
    private readonly IUserIdentity _userIdentity = userIdentity;

    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        => await _userIdentity.LoginAsync(command.Request.Email, command.Request.Password);
}
