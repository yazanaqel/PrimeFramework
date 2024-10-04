using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Shared;

namespace Application.Features.Authentication.RegisterUser;

internal sealed class RegisterUserCommandHandler(IUserRepository userRepository) : ICommandHandler<RegisterUserCommand, string>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<string>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        => await _userRepository.RegisterAsync(command.Request.Email, command.Request.Password);
}
