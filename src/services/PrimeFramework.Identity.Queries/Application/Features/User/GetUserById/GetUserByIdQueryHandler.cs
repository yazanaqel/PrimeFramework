using Application.Abstractions.Messaging;
using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Domain.Entities.User;
using Domain.Specifications.User;

namespace Application.Features.User.GetUserById;

internal sealed class GetUserByIdQueryHandler(IReadRepository<AppUser> userIdentity) : IQueryHandler<GetUserByIdQuery,GetUserByIdResponse>
{
    private readonly IReadRepository<AppUser> _userIdentity = userIdentity;
    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery request,CancellationToken cancellationToken)
    {
        var spec = new GetUserByIdSpecification(request.UserId);

        var user = await _userIdentity.FirstOrDefaultAsync(spec, cancellationToken);

        if(user is null)
            //throw new NotFoundException(nameof(User),request.UserId);
            throw new Exception($"User With Id : {request.UserId} Not Found!");

        return Result.Success(new GetUserByIdResponse(user.Id,user.Email,user.UserName));

    }


}