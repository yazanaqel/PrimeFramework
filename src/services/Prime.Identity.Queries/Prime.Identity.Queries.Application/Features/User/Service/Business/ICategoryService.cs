using CSharpFunctionalExtensions;
using Prime.Identity.Queries.Application.Features.Category;

namespace Prime.Identity.Queries.Application.Features.User.Service.Business;

public interface ICategoryService
{
    Task<Result<List<GetAllCategoriesResponse>>> GetAllCategoriesAsync(CancellationToken ct = default);

}
