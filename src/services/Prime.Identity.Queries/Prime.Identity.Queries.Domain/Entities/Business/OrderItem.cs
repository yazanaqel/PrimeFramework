using Prime.Identity.Queries.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prime.Identity.Queries.Domain.Entities.Business;

public class OrderItem
{
    public Guid Id { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }

    //Navigational properties
    public Product? Product { get; set; }
    public Guid ProductId { get;  set; }
    public Order? Order { get;  set; }
    public Guid OrderId { get;  set; }
    public OrderItemStatus OrderItemStatus { get; set; }
}
