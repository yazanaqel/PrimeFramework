using Ardalis.Specification;
using Prime.Identity.Queries.Domain.Entities.Business;

namespace Prime.Identity.Queries.Domain.Specifications.Business;

public sealed class GetUserOrdersSpec : Specification<Order>
{
    public GetUserOrdersSpec(Guid userId)
    {
        Query.Where((u) => (u.UserId == userId));

        Query.Include(o => o.OrderItems).Include(c => c.Store);

        Query.Include(o => o.OrderItems).ThenInclude(s => s.Product);

    }
}
