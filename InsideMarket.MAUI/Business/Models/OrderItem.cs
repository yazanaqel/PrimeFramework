namespace InsideMarket.MAUI.Business.Models;

public class OrderItem
{
    public Guid Id { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
}

public class CreateOrderRequest
{
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
public class GetUserOrdersResponse
{
    public string StoreName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
}
public class GetStoreOrdersResponse
{
    public string UserId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
}
