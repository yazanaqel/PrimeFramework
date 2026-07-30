using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Prime.Identity.Domain.Entities.Orders;
using Prime.Identity.Domain.Repositories;
using System.Threading.Channels;

namespace Prime.Identity.Infrastructure.Services;

public class OrderItemService(ApplicationDbContext applicationDbContext) : IOrderItemService
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

    public async Task<bool> ChangeOrderItemStatus(List<ChangeOrderItemStatus> changeOrderItemStatus,CancellationToken ct)
    {


        foreach(var change in changeOrderItemStatus)
        {
            await _applicationDbContext.Set<OrderItem>()
                .Where(x => x.Id == change.OrderItemId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(i => i.OrderItemStatus,change.OrderItemStatus));
        }


        return true;

    }
}
