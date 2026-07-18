namespace Prime.Identity.Queries.Domain.Entities.Business;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }

    // Navigation properties
    public Guid StoreId { get; set; }
    public Store? Store { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
}
