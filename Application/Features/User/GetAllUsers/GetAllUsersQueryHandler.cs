using Application.Abstractions.Messaging;
using Application.Repositories;
using CSharpFunctionalExtensions;
using Domain.Entities.Users;
using MediatR;

namespace Application.Features.User.GetAllUsers;

internal sealed class GetAllUsersQueryHandler(IUserIdentity userIdentity) : IQueryHandler<GetAllUsersQuery,IEnumerable<AppUser>>
{
    private readonly IUserIdentity _userIdentity = userIdentity;

    public async Task<Result<IEnumerable<AppUser>>> Handle(GetAllUsersQuery request,CancellationToken cancellationToken)
    {
        var users = await _userIdentity.GetAllUsersAsync(cancellationToken);

        var res =  Result.Success(users);

        return res;
    }
}
