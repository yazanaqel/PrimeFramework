using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Prime.Identity.Queries.Application.Abstractions.Auth;
using Prime.Identity.Queries.Application.Features.Category;
using Prime.Identity.Queries.Domain.Specifications.Business;

namespace Prime.Identity.Queries.Application.Features.User.Service.Business;

public class CategoryService(
    IReadRepository<Domain.Entities.Business.Category> repo,
    IReadRepository<Domain.Entities.Business.Store> storeRepository,
    ICurrentUserService currentUserService) : ICategoryService
{
    private readonly IReadRepository<Domain.Entities.Business.Category> _repo = repo;
    private readonly IReadRepository<Domain.Entities.Business.Store> _storeRepository = storeRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Result<List<GetAllCategoriesResponse>>> GetAllCategoriesAsync(CancellationToken ct = default)
    {

        var userId = Guid.TryParse(_currentUserService.UserId,out var parsedUserId)
? parsedUserId : throw new InvalidOperationException("Invalid user ID");

        var spec1 = new GetAllCategoriesSpec();

        var categories = await _repo.ListAsync(spec1,ct);


        var spec2 = new GetOwnerStoreByIdSpec(parsedUserId);

        var store = await _storeRepository.FirstOrDefaultAsync(spec2,ct);


        var response = new List<GetAllCategoriesResponse>();

        if(store is not null)
        {
            response = categories
            .Where(i => i.Id == store.CategoryId)
            .Select(p => new GetAllCategoriesResponse(
                p.Id,
                p.Name,
                p.Description,
                p.SubCategories.Select(c => new ChildrenResponse(
                    c.Id,
                    c.Name,
                    c.Description,
                    c.ParentCategoryId,
                    null
                )).ToList()
            ))
            .ToList();

            return Result.Success(response);
        }

        response = categories
            .Where(i => i.ParentCategoryId is null)
            .Select(p => new GetAllCategoriesResponse(
                p.Id,
                p.Name,
                p.Description,
                p.SubCategories.Select(c => new ChildrenResponse(
                    c.Id,
                    c.Name,
                    c.Description,
                    c.ParentCategoryId,
                    null
                )).ToList()
            ))
            .ToList();



        return Result.Success(response);
    }
}
