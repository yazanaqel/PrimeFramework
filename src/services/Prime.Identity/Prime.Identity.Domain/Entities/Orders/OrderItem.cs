using Domain.Primitives;
using Prime.Identity.Domain.Entities.Enums;
using Prime.Identity.Domain.Entities.Products;
using Prime.Identity.Domain.Entities.Stores;

namespace Prime.Identity.Domain.Entities.Orders;


public sealed class OrderItem : Entity<OrderItemId>, IAuditableEntity
{

    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }

    //Navigational properties
    public Product Product { get; private set; }
    public ProductId ProductId { get; private set; }
    public Order Order { get; private set; }
    public OrderId OrderId { get; private set; }
    public OrderItemStatus OrderItemStatus { get; private set; } 

    // EF Core
    private OrderItem() { }

    // Factory
    public static OrderItem Create(ProductId productId,OrderId orderId,decimal unitPrice,int quantity)
    {
        if(quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        //if(unitPrice <= 0)
        //    throw new ArgumentException("Unit price must be greater than zero.");

        return new OrderItem
        {
            Id = OrderItemId.New(),
            ProductId = productId,
            OrderId = orderId,
            UnitPrice = unitPrice,
            Quantity = quantity,
            CreatedAt = DateTime.UtcNow,
            OrderItemStatus = OrderItemStatus.Suspended,
        };
    }

}