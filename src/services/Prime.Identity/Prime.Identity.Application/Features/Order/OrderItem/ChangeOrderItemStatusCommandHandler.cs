using Application.Abstractions.Messaging;
using CSharpFunctionalExtensions;
using Prime.Identity.Domain.Entities.Orders;
using Prime.Identity.Domain.Repositories;

namespace Prime.Identity.Application.Features.Order.OrderItem;

internal sealed class ChangeOrderItemStatusCommandHandler(IOrderItemService orderItemService) : ICommandHandler<ChangeOrderItemStatusCommand,bool>
{
    private readonly IOrderItemService _orderItemService = orderItemService;

    public async Task<Result<bool>> Handle(ChangeOrderItemStatusCommand command,CancellationToken ct)
    {

        List<ChangeOrderItemStatus> changeOrderItemStatus = new List<ChangeOrderItemStatus>();

        foreach(var item in command.Request)
        {
            if(OrderItemId.TryParse(item.OrderItemId,out var parsedOrderId))
            {
                changeOrderItemStatus.Add(new ChangeOrderItemStatus(parsedOrderId,item.OrderItemStatus));
            }
            else
            {
                return Result.Failure<bool>($"Invalid product ID: {item.OrderItemId}");
            }
        }

        bool result = await _orderItemService.ChangeOrderItemStatus(changeOrderItemStatus,ct);


        return Result.Success(true);
    }

}