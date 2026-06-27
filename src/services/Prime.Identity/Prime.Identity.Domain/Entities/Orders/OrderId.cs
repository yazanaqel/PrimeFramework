namespace Prime.Identity.Domain.Entities.Orders;

public readonly record struct OrderId(Guid Value)
{
    public static OrderId New() => new(Guid.NewGuid());

    public static bool TryParse(string? input,out OrderId orderId)
    {
        if(Guid.TryParse(input,out var guid))
        {
            orderId = new OrderId(guid);
            return true;
        }

        orderId = default;
        return false;
    }
}