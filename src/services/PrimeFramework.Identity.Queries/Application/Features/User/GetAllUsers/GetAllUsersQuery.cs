using Application.Abstractions.Messaging;
using Application.Pagination;

namespace Application.Features.User.GetAllUsers;

public sealed record GetAllUsersQuery(GetAllUsersQueryRequest Request,CancellationToken CancellationToken) : IQuery<CursorPageResponse<GetAllUsersQueryResponse>>;