using Application.Features.User.GetAllUsers;
using Application.Features.User.GetUserById;
using Application.Pagination;
using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Domain.Entities.User;
using Domain.Specifications.User;
using Prime.Identity.Queries.Application.Abstractions.Cache;

namespace Prime.Identity.Queries.Application.Features.User.Service;

public sealed class UserService(IReadRepository<AppUser> userIdentity,ICacheService cacheService) : IUserService
{
    private readonly IReadRepository<AppUser> _userIdentity = userIdentity;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<CursorPageResponse<GetAllUsersResponse>>> GetAllUsersAsync(GetAllUsersRequest request,CancellationToken ct)
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


    public async Task<Result<GetUserByIdResponse>> GetUserByIdAsync(Guid userId,CancellationToken ct)
    {
        var cacheKey = $"user:{userId}";

        var cached = await _cacheService.GetAsync<GetUserByIdResponse>(cacheKey, ct);

        if(cached is not null)
            return cached;

        var spec = new GetUserByIdSpecification(userId);

        var user = await _userIdentity.FirstOrDefaultAsync(spec,ct);

        if(user is null)
            //throw new NotFoundException(nameof(User),userId);
            throw new Exception($"User With Id : {userId} Not Found!");

        var response = new GetUserByIdResponse(user.Id,user.Email,user.UserName);

        await _cacheService.SetAsync(cacheKey,response,TimeSpan.FromMinutes(5),ct);

        return Result.Success(response);
    }
}
