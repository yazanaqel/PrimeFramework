using Application.Features.User.GetAllUsers;
using Application.Features.User.GetUserById;
using Application.Pagination;
using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Domain.Entities.User;
using Domain.Specifications.User;
using Prime.Identity.Queries.Application.Abstractions.Auth;
using Prime.Identity.Queries.Application.Abstractions.Cache;
using Prime.Identity.Queries.Application.Features.Store.GetOwnerStore;
using Prime.Identity.Queries.Domain.Specifications.Business;
using System.Buffers.Text;

namespace Prime.Identity.Queries.Application.Features.User.Service;

public sealed class UserService(IReadRepository<AppUser> userIdentity,ICacheService cacheService,ICurrentUserService currentUserService,IReadRepository<Domain.Entities.Business.Store> storeRepository) : IUserService
{
    private readonly IReadRepository<AppUser> _userIdentity = userIdentity;
    private readonly ICacheService _cacheService = cacheService;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IReadRepository<Domain.Entities.Business.Store> _storeRepository = storeRepository;
    private const string baseUrl = "https://localhost:7104/";
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
        var cacheKey = $"_user_:{userId}";

        var cached = await _cacheService.GetAsync<GetUserByIdResponse>(cacheKey, ct);

        if(cached is not null)
            return cached;

        var spec = new GetUserByIdSpecification(userId);

        var user = await _userIdentity.FirstOrDefaultAsync(spec,ct);

        if(user is null)
            //throw new NotFoundException(nameof(User),userId);
            throw new Exception($"User With Id : {userId} Not Found!");

        var store = await _storeRepository.FirstOrDefaultAsync(new GetOwnerStoreByIdSpec(userId),ct);

        if(store is not null)
        {
            var storeResponse = new GetOwnerStoreResponse(
                store.Id,
                store.UserId,
                store.CategoryId,
                store.Name,
                $"{baseUrl}{store.ImageCover}",
                $"{baseUrl}{store.Image}",
                store.Description,
                store.Address,
                store.IsShippingAvailable,
                store.City,
                store.StoreStatus,
                store.CreatedAt,
                store.ModifiedAt
                );

            var response1 = new GetUserByIdResponse(user.Id,user.Email,user.UserName,storeResponse);

            await _cacheService.SetAsync(cacheKey,response1,TimeSpan.FromMinutes(5),ct);

            return Result.Success(response1);
        }

        var response2 = new GetUserByIdResponse(user.Id,user.Email,user.UserName,null);

        await _cacheService.SetAsync(cacheKey,response2,TimeSpan.FromMinutes(5),ct);

        return Result.Success(response2);
    }

    public async Task<Result<GetUserProfileResponse>> GetUserProfile(CancellationToken ct = default)
    {
        var userId = Guid.TryParse(_currentUserService.UserId,out var parsedUserId)
? parsedUserId : throw new InvalidOperationException("Invalid user ID");

        var spec = new GetUserByIdSpecification(userId);

        var user = await _userIdentity.FirstOrDefaultAsync(spec,ct);

        if(user is null)
            //throw new NotFoundException(nameof(User),userId);
            throw new Exception($"User With Id : {userId} Not Found!");

        var response = new GetUserProfileResponse(user.Id.ToString(),user.Email,user.UserName);

        return Result.Success(response);
    }
}
