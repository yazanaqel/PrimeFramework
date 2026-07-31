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

public class GetProductByIdResponse
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Store Store { get; set; } = new();
    public List<Product>? SimilarProducts { get; set; } = new List<Product>();
}

public class GetAllProductsResponse
{
    public string CategoryName { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}