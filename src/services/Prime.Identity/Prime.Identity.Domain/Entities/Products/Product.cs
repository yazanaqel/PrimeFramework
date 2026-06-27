using Domain.Primitives;
using Prime.Identity.Domain.Entities.Categories;
using Prime.Identity.Domain.Entities.Stores;

namespace Prime.Identity.Domain.Entities.Products;

public class Product : Entity<ProductId>, IAuditableEntity
{
    private Product() { }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public int StockQuantity { get; private set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }

    // Navigation properties
    public StoreId StoreId { get; private set; }
    public Store Store { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public Category Category { get; private set; }

}
