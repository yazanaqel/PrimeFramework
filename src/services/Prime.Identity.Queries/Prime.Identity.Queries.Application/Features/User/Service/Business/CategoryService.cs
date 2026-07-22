using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Prime.Identity.Queries.Application.Features.Category;
using Prime.Identity.Queries.Domain.Specifications.Business;

namespace Prime.Identity.Queries.Application.Features.User.Service.Business;

public class CategoryService(IReadRepository<Domain.Entities.Business.Category> repo) : ICategoryService
{
    private readonly IReadRepository<Domain.Entities.Business.Category> _repo = repo;

    public async Task<Result<List<GetAllCategoriesResponse>>> GetAllCategoriesAsync(CancellationToken ct = default)
    {
        var spec = new GetAllCategoriesSpec();

        var categories = await _repo.ListAsync(spec,ct);

        var categoriesTree = categories
            .Where(i => i.ParentCategoryId is null)
            .Select(p => new GetAllCategoriesResponse(
                p.Id,
                p.Name,
                p.Description,
                p.SubCategories.Select(c => new ChildrenResponse(
                    c.Id,
                    c.Name,
                    c.Description,
                    c.ParentCategoryId
                )).ToList()
            ))
            .ToList();



        return Result.Success(categoriesTree);
    }
}
