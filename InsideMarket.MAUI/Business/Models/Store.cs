using InsideMarket.MAUI.Enums;

namespace InsideMarket.MAUI.Business.Models;

public class Store
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageCover { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool IsShippingAvailable { get; set; }
    public City City { get; set; }
    public StoreStatus StoreStatus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }

}

public class GetAllStoresRequest
{
    public string Search { get; set; } = string.Empty;
}