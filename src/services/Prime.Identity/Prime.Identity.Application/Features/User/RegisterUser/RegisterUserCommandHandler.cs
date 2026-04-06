using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Features.User.RefreshToken;
using CSharpFunctionalExtensions;
using Domain.Entities.Users;
using Domain.Repositories;
using Domain.ValueObjects;
using Prime.Identity.Domain.Entities.Users;

namespace Application.Features.Authentication.RegisterUser;

internal sealed class RegisterUserCommandHandler(IUserService userService,IDomainEventDispatcher domainEventDispatcher) : ICommandHandler<RegisterUserCommand,RefreshTokenResponse?>
{
    private readonly IUserService _userService = userService;
    private readonly IDomainEventDispatcher _domainEventDispatcher = domainEventDispatcher;

    public async Task<Result<RefreshTokenResponse?>> Handle(RegisterUserCommand command,CancellationToken ct)
    {

        if(Email.TryCreate(command.Request.Email,out var email))
        {
            var availableEmail = await _userService.IsEmailAvailable(email.Value,ct);

            if(!availableEmail)
                return Result.Failure<RefreshTokenResponse?>("Email Is Not Available");
        }
        else
        {
            return Result.Failure<RefreshTokenResponse?>("Email Is Not Valid");
        }

        AppUser appUser = new AppUser(UserId.New(),email,command.Request.Email,command.Request.Password);

        var authResult = await _userService.RegisterAsync(
            appUser: appUser,ct);

        await _domainEventDispatcher.DispatchAsync(appUser.DomainEvents);

        appUser.ClearDomainEvents();

        return Result.Success(authResult);
    }
}
