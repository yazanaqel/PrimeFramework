using Application.Abstractions.Messaging;
using Application.Repositories;
using Domain.Shared;

namespace Application.Features.Authentication.RegisterUser;

internal sealed class RegisterUserCommandHandler(IUserIdentity userIdentity) : ICommandHandler<RegisterUserCommand, string>
{
    private readonly IUserIdentity _userIdentity = userIdentity;

    public async Task<Result<string>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        => await _userIdentity.RegisterAsync(command.Request.Email, command.Request.Password);
}
