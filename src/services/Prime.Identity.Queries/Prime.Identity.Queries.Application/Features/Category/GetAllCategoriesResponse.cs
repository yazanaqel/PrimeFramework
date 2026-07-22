namespace Prime.Identity.Queries.Application.Features.Category;

public record GetAllCategoriesResponse(Guid CategoryId,string Name,string Description,List<ChildrenResponse> ChildrenResponses);
public record ChildrenResponse(Guid CategoryId,string Name,string Description,Guid? ParentCategoryId);
