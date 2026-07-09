using System;
using System.Collections.Generic;
using System.Text;

namespace InsideMarket.MAUI.Business.Models;

public class Product
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int StockQuantity { get; set; }
    public Guid CategoryId { get; set; } = Guid.TryParse("EFBB4FD3-8ED1-4110-9ECA-476FE3B38E21", out var categoryId) ? categoryId : Guid.Empty;

}
