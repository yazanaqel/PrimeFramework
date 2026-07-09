using InsideMarket.MAUI.Business.Models;
using System.Net.Http.Json;
using static InsideMarket.MAUI.Route.RouteGate;

namespace InsideMarket.MAUI.Business.Services;

public class ProductService(IHttpClientFactory httpClientFactory) : IProductService
{
    private readonly HttpClient _httpRead = httpClientFactory.CreateClient("Read");
    private readonly HttpClient _httpWrite = httpClientFactory.CreateClient("Write");
    public async Task<bool> CreateProduct(Product product)
    {
        var response = await _httpWrite.PostAsJsonAsync(ProductRouteGate.CreateProduct,product);

        if(!response.IsSuccessStatusCode)
            return false;

        else
            return true;
    }

    public async Task<Product> GetProductById(Guid productId)
    {
        var response = await _httpRead.GetAsync($"{ProductRouteGate.GetProductById}/{productId}");

        if(response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<Product>();

            if(result is null)
                return new Product();

            return result;
        }
        else
        {
            return new Product();
        }
    }

    public async Task<List<Product>> GetStoreProducts(Guid? storeId)
    {
        var url = storeId is null
            ? $"{ProductRouteGate.GetStoreProducts}"
            : $"{ProductRouteGate.GetStoreProductsById}/{storeId}";

        var response = await _httpRead.GetAsync(url);

        if(response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<List<Product>>();

            if(result is null)
                return new List<Product>();

            return result;
        }
        else
        {
            return new List<Product>();
        }
    }
}
