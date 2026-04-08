using Application.Features.User.GetAllUsers;
using Application.Features.User.GetUserById;
using Application.Pagination;
using CSharpFunctionalExtensions;

namespace Prime.Identity.Queries.Application.Features.User.Service;

public interface IUserService
{
    Task<Result<CursorPageResponse<GetAllUsersResponse>>> GetAllUsersAsync(GetAllUsersRequest request,CancellationToken ct);
    Task<Result<GetUserByIdResponse>> GetUserByIdAsync(Guid userId,CancellationToken ct);
}
