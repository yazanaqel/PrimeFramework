using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Features.User.RefreshToken;
using CSharpFunctionalExtensions;
using Domain.Entities.Users;
using Domain.Repositories;
using Domain.ValueObjects;
using Prime.Identity.Domain.Entities.Users;

namespace Application.Features.Authentication.RegisterUser;

internal sealed class RegisterUserCommandHandler(IUserService userService,IDomainEventDispatcher domainEventDispatcher) : ICommandHandler<RegisterUserCommand,bool>
{
    private readonly IUserService _userService = userService;
    private readonly IDomainEventDispatcher _domainEventDispatcher = domainEventDispatcher;

    public async Task<Result<bool>> Handle(RegisterUserCommand command,CancellationToken ct)
    {

        if(Email.TryCreate(command.Request.Email,out var email))
        {
            AppUser appUser = new AppUser(
                UserId.New(),
                email,
                command.Request.Email,
                command.Request.Password);

            bool result = await _userService.RegisterAsync(appUser: appUser,ct);

            await _domainEventDispatcher.DispatchAsync(appUser.DomainEvents);

            appUser.ClearDomainEvents();

            return Result.Success(result);
        }

        return Result.Failure<bool>("Error");

    }
}
