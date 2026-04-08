using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Domain.Entities.User;
using Domain.Specifications.User;

namespace Application.Features.User.GetUserById;

public sealed class GetUserByIdQueryHandler(IReadRepository<AppUser> userIdentity)
{
    private readonly IReadRepository<AppUser> _userIdentity = userIdentity;
    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdRequest request,CancellationToken ct)
    {
        var spec = new GetUserByIdSpecification(request.UserId);

        var user = await _userIdentity.FirstOrDefaultAsync(spec,ct);

        if(user is null)
            //throw new NotFoundException(nameof(User),request.UserId);
            throw new Exception($"User With Id : {request.UserId} Not Found!");

        return Result.Success(new GetUserByIdResponse(user.Id,user.Email,user.UserName));

    }


}