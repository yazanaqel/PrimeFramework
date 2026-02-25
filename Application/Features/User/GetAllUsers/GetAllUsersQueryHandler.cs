using Application.Abstractions.Messaging;
using Application.Repositories;
using CSharpFunctionalExtensions;

namespace Application.Features.User.GetAllUsers;

internal sealed class GetAllUsersQueryHandler(IUserIdentity userIdentity) : IQueryHandler<GetAllUsersQuery,IEnumerable<GetAllUsersResponse>>
{
    private readonly IUserIdentity _userIdentity = userIdentity;

    public async Task<Result<IEnumerable<GetAllUsersResponse>>> Handle(GetAllUsersQuery request,CancellationToken cancellationToken)
    {
        var users = await _userIdentity.GetAllUsersAsync();

        return Result.Success(users.Select(u => new GetAllUsersResponse(u.Id,u.Email.Value,u.UserName)));
    }
}
