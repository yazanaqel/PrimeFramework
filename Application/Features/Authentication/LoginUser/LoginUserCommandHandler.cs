using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Shared;

namespace Application.Features.Authentication.LoginUser;

internal sealed class LoginUserCommandHandler(IUserRepository userRepository) : ICommandHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        => await _userRepository.LoginAsync(command.Request.Email, command.Request.Password);
}
