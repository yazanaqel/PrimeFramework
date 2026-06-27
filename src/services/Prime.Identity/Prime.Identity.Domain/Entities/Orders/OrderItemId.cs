namespace Prime.Identity.Domain.Entities.Orders;


public readonly record struct OrderItemId(Guid Value)
{
    public static OrderItemId New() => new(Guid.NewGuid());

    public static bool TryParse(string? input,out OrderItemId orderItemId)
    {
        if(Guid.TryParse(input,out var guid))
        {
            orderItemId = new OrderItemId(guid);
            return true;
        }

        orderItemId = default;
        return false;
    }
}