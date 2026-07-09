using InsideMarket.MAUI.Business.Models;

namespace InsideMarket.MAUI.Business.Services;

public interface IProductService
{
    Task<Product> GetProductById(Guid productId);
    Task<List<Product>> GetStoreProducts(Guid? storeId = null);
    Task<bool> CreateProduct(Product product);
}
