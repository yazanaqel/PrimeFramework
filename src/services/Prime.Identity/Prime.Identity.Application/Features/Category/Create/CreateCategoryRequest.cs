using Prime.Identity.Domain.Entities.Categories;

namespace Prime.Identity.Application.Features.Category.Create;

public record CreateCategoryRequest(string Name,string Description,CategoryId? ParentCategoryId);

