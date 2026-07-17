namespace InsideMarket.MAUI.Business.Models;

public class Product
{
    public string ProductId { get; set; } = string.Empty;
    public string CategoryId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public Stream ImageStream { get; set; } = Stream.Null;
    public string ImageFileName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int StockQuantity { get; set; }

}
