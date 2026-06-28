using Domain.Primitives;
using Prime.Identity.Domain.Entities.Categories;
using Prime.Identity.Domain.Entities.Enums;
using Prime.Identity.Domain.Entities.Orders;
using Prime.Identity.Domain.Entities.Products;
using Prime.Identity.Domain.Entities.Users;

namespace Prime.Identity.Domain.Entities.Stores;

public class Store : Entity<StoreId>, IAuditableEntity
{
    private Store() { }

    public string Name { get; private set; } = string.Empty;
    public string ImageCover { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public bool IsShippingAvailable { get; private set; }
    public City City { get; private set; }
    public StoreStatus StoreStatus { get; private set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }

    //Navigation properties

    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    private readonly List<Order> _orders = new();
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();
    public CategoryId CategoryId { get; private set; }
    public Category Category { get; private set; }
    //public AppUser AppUser { get; private set; }
    public UserId UserId { get; private set; }


    public static Store Create(
        UserId userId,
        CategoryId categoryId,
    string name,
    string description,
    string imageCover,
    string image,
    string address,
    bool isShippingAvailable,
    City city)
    {

        return new Store
        {
            Id = StoreId.New(),
            UserId = userId,
            CategoryId = categoryId,
            Name = name,
            Description = description,
            ImageCover = imageCover,
            Image = image,
            Address = address,
            IsShippingAvailable = isShippingAvailable,
            City = city,
            StoreStatus = StoreStatus.Suspended
        };
    }
}
