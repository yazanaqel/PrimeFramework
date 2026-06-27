using Domain.Entities.Users;
using Domain.Primitives;
using Prime.Identity.Domain.Entities.Products;
using Prime.Identity.Domain.Entities.Stores;
using Prime.Identity.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Identity.Domain.Entities.Orders;

public class Order : Entity<OrderId>, IAuditableEntity
{
    private Order() { }

    public long OrderNumber { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalPrice { get; private set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }

    //Navigation properties
    public Store Store { get; private set; }
    public StoreId StoreId { get; private set; }
    public Product Product { get; private set; }
    public ProductId ProductId { get; private set; }
    //public AppUser AppUser { get; private set; }
    public UserId UserId { get; private set; }
    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

}
