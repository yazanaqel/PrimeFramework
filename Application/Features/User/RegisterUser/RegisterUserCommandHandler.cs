using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Repositories;
using CSharpFunctionalExtensions;
using Domain.Entities.Users;

namespace Application.Features.Authentication.RegisterUser;

internal sealed class RegisterUserCommandHandler(IUserIdentity userIdentity,IUnitOfWork unitOfWork) : ICommandHandler<RegisterUserCommand,string>
{
    private readonly IUserIdentity _userIdentity = userIdentity;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<string>> Handle(RegisterUserCommand command,CancellationToken cancellationToken)
    {
        var result = await _userIdentity.RegisterAsync(
            new AppUser(command.Request.Email,command.Request.Email,command.Request.Password));

        if(string.IsNullOrWhiteSpace(result))
        {
            return Result.Failure<string>("Failed to register user.");
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success(result);
    }
}
