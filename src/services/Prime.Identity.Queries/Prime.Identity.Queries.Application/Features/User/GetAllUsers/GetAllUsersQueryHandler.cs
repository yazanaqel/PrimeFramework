using Application.Pagination;
using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Domain.Entities.User;
using Domain.Specifications.User;

namespace Application.Features.User.GetAllUsers;

public sealed class GetAllUsersQueryHandler(IReadRepository<AppUser> userIdentity)
{
    private readonly IReadRepository<AppUser> _userIdentity = userIdentity;

    public async Task<Result<CursorPageResponse<GetAllUsersResponse>>> Handle(GetAllUsersRequest request,CancellationToken ct)
    {
        UserCursor? after = null;

        if(!string.IsNullOrWhiteSpace(request.After))
            after = CursorEncoder.Decode<UserCursor>(request.After);

        var spec = new UsersCursorSpecification(
            after,
            request.Size,
            request.Search,
            request.SortBy,
            request.Descending);

        var users = await _userIdentity.ListAsync(spec,ct);

        bool hasMore = users.Count > request.Size;

        IReadOnlyList<AppUser> pageItems = hasMore ? users.Take(request.Size).ToList() : users;

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

        return new CursorPageResponse<GetAllUsersResponse>
        {
            Items = pageItems.Select(u => new GetAllUsersResponse(u.Id,u.Email,u.CreatedAt)).ToList(),
            HasMore = hasMore,
            NextCursor = nextCursor
        };
    }
}