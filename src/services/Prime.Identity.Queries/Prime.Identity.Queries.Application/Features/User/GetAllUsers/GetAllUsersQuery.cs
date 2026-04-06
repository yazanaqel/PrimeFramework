using Application.Abstractions.Messaging;
using Application.Pagination;

namespace Application.Features.User.GetAllUsers;

public sealed record GetAllUsersQuery(GetAllUsersRequest Request,CancellationToken CancellationToken) : IQuery<CursorPageResponse<GetAllUsersResponse>>;