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

public class ProductInBasket
{
    public string ProductId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }

}

public class GetCategorizedProductsResponse
{
    public string CategoryId { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = new List<Product>();
}