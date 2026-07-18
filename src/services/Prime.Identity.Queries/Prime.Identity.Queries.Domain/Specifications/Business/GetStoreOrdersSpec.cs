using Ardalis.Specification;
using Prime.Identity.Queries.Domain.Entities.Business;

namespace Prime.Identity.Queries.Domain.Specifications.Business;


public sealed class GetStoreOrdersSpec : Specification<Order>
{
    public GetStoreOrdersSpec(Guid storeId)
    {
        Query.Where((u) => (u.StoreId == storeId));

        Query.Include(o => o.OrderItems)
             .ThenInclude(oi => oi.Product);


        //Query.Include(o => o.OrderItems).Include(c => c.Store);

        //Query.Include(o => o.OrderItems).ThenInclude(s => s.Product);
    }
}