using InsideMarket.MAUI.Business.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static InsideMarket.MAUI.Route.RouteGate;

namespace InsideMarket.MAUI.Business.Services;

public class ProductService(IHttpClientFactory httpClientFactory) : IProductService
{
    private readonly HttpClient _httpRead = httpClientFactory.CreateClient("Read");
    private readonly HttpClient _httpWrite = httpClientFactory.CreateClient("Write");
    public async Task<bool> CreateProduct(Product product)
    {
        using var form = new MultipartFormDataContent();

        // Add normal fields
        form.Add(new StringContent(product.CategoryId),"CategoryId");
        form.Add(new StringContent(product.Name),"Name");
        form.Add(new StringContent(product.Description),"Description");
        form.Add(new StringContent(product.UnitPrice.ToString()),"UnitPrice");
        form.Add(new StringContent(product.StockQuantity.ToString()),"StockQuantity");

        // Add file
        

        var fileContent = new StreamContent(product.ImageStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/*");

        form.Add(fileContent,"ImageFile",product.ImageFileName);

        var response = await _httpWrite.PostAsync(ProductRouteGate.CreateProduct,form);
        response.EnsureSuccessStatusCode();

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
