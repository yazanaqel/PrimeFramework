using Application.Abstractions.Messaging;
using Application.Pagination;
using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Domain.Entities.User;
using Domain.Specifications.User;

namespace Application.Features.User.GetAllUsers;

internal sealed class GetAllUsersQueryHandler(IReadRepository<AppUser> userIdentity) : IQueryHandler<GetAllUsersQuery,CursorPageResponse<GetAllUsersQueryResponse>>
{
    private readonly IReadRepository<AppUser> _userIdentity = userIdentity;

    public async Task<Result<CursorPageResponse<GetAllUsersQueryResponse>>> Handle(GetAllUsersQuery request,CancellationToken ct)
    {
        UserCursor? after = null;

        if(!string.IsNullOrWhiteSpace(request.Request.After))
            after = CursorEncoder.Decode<UserCursor>(request.Request.After);

        var spec = new UsersCursorSpecification(
            after,
            request.Request.Size,
            request.Request.Search,
            request.Request.SortBy,
            request.Request.Descending);

        var users = await _userIdentity.ListAsync(spec,ct);

        bool hasMore = users.Count > request.Request.Size;

        IReadOnlyList<AppUser> pageItems = hasMore ? users.Take(request.Request.Size).ToList() : users;

        string? nextCursor = null;

        if(hasMore)
        {
            AppUser last = pageItems[^1];

            nextCursor = CursorEncoder.Encode(new UserCursor
            {
                CreatedAt = last.CreatedAt,
                Id = last.Id
            });
        }

        return new CursorPageResponse<GetAllUsersQueryResponse>
        {
            Items = pageItems.Select(u => new GetAllUsersQueryResponse(u.Id,u.Email,u.CreatedAt)).ToList(),
            HasMore = hasMore,
            NextCursor = nextCursor
        };
    }
}