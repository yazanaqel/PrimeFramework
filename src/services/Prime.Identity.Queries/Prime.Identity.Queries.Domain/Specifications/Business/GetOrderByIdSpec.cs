using Ardalis.Specification;
using Prime.Identity.Queries.Domain.Entities.Business;

namespace Prime.Identity.Queries.Domain.Specifications.Business;

public sealed class GetOrderByIdSpec : Specification<Order>
{
    public GetOrderByIdSpec(Guid orderId)
    {
        Query.Where((u) => (u.Id == orderId));

        Query.Include(o => o.OrderItems).ThenInclude(s => s.Product);

    }
}
