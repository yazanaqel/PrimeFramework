namespace Prime.Identity.Queries.Domain.Entities.Business;

public class Order
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }

    //Navigation properties
    public Store? Store { get; set; }
    public Guid StoreId { get; set; }
    public Guid UserId { get; set; }
    public IReadOnlyCollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();



}
