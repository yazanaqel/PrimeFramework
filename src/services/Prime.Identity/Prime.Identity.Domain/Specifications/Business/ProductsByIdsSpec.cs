using Ardalis.Specification;
using Prime.Identity.Domain.Entities.Products;

namespace Prime.Identity.Domain.Specifications.Business;

public class ProductsByIdsSpec : Specification<Product>
{
    public ProductsByIdsSpec(IEnumerable<ProductId> productIds)
    {
        Query.Where(p => productIds.Contains(p.Id));
        Query.Include(p => p.Store);
    }
}
