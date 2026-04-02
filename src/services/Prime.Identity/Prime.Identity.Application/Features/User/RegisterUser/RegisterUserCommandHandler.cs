using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Features.User.RefreshToken;
using Application.Repositories;
using CSharpFunctionalExtensions;
using Domain.Entities.Users;
using Domain.ValueObjects;

namespace Application.Features.Authentication.RegisterUser;

internal sealed class RegisterUserCommandHandler(IUserIdentity userIdentity,IDomainEventDispatcher domainEventDispatcher) : ICommandHandler<RegisterUserCommand,RefreshTokenResponse?>
{
    private readonly IUserIdentity _userIdentity = userIdentity;
    private readonly IDomainEventDispatcher _domainEventDispatcher = domainEventDispatcher;

    public async Task<Result<RefreshTokenResponse?>> Handle(RegisterUserCommand command,CancellationToken cancellationToken)
    {

        if(Email.TryCreate(command.Request.Email,out var email))
        {
            var availableEmail = await _userIdentity.IsEmailAvailable(email.Value,cancellationToken);

            if(!availableEmail)
                return Result.Failure<RefreshTokenResponse?>("Email Is Not Available");
        }
        else
        {
            return Result.Failure<RefreshTokenResponse?>("Email Is Not Valid");
        }

        AppUser appUser = new AppUser(email,command.Request.Email,command.Request.Password);

        var authResult = await _userIdentity.RegisterAsync(
            appUser: appUser,cancellationToken);

        await _domainEventDispatcher.DispatchAsync(appUser.DomainEvents);

        appUser.ClearDomainEvents();

        return Result.Success(authResult);
    }
}
