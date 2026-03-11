using Application.Abstractions.Messaging;
using Application.Exceptions;
using Application.Repositories;
using CSharpFunctionalExtensions;
using Domain.Entities.Users;

namespace Application.Features.User.GetUserById;

internal sealed class GetUserByIdQueryHandler(IUserIdentity userIdentity) : IQueryHandler<GetUserByIdQuery,GetUserByIdResponse>
{
    private readonly IUserIdentity _userIdentity = userIdentity;
    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery request,CancellationToken cancellationToken)
    {
        var spec = new GetUserByIdSpecification(request.UserId);

        var user = await _userIdentity.GetAsync(spec,cancellationToken);

        if(user is null)
            throw new NotFoundException(nameof(User),request.UserId);

        return Result.Success(new GetUserByIdResponse(user.Id,user.Email.Value,user.UserName));

    }


}