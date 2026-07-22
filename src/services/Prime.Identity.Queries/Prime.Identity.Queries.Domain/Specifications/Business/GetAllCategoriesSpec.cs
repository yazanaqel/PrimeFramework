using Ardalis.Specification;
using Prime.Identity.Queries.Domain.Entities.Business;

namespace Prime.Identity.Queries.Domain.Specifications.Business;

public class GetAllCategoriesSpec : Specification<Category>
{
    public GetAllCategoriesSpec()
    {
        Query.OrderBy(s => s.Name);

        Query.Include(s => s.SubCategories);

        Query.Include(s => s.Products);
    }
}
