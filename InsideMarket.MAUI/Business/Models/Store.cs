using InsideMarket.MAUI.Enums;

namespace InsideMarket.MAUI.Business.Models;

public class Store
{
    public string StoreId { get; set; } = string.Empty;
    public string CategoryId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ImageCover { get; set; } = string.Empty;
    public Stream ImageCoverStream { get; set; } = Stream.Null;
    public string ImageCoverFileName { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public Stream ImageStream { get; set; } = Stream.Null;
    public string ImageFileName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool IsShippingAvailable { get; set; }
    public City City { get; set; } = City.Damascus;
    public StoreStatus StoreStatus { get; set; } = StoreStatus.Suspended;

}

public class GetAllStoresRequest
{
    public string Search { get; set; } = string.Empty;
}