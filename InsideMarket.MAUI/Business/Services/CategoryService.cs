using InsideMarket.MAUI.Business.Models;
using System.Net.Http.Json;
using static InsideMarket.MAUI.Route.RouteGate;

namespace InsideMarket.MAUI.Business.Services;

public class CategoryService(IHttpClientFactory httpClientFactory) : ICategoryService
{
    private readonly HttpClient _http = httpClientFactory.CreateClient("Read");

    public async Task<List<Category>> GetAllCategories()
    {
        var response = await _http.GetAsync(CategoryRouteGate.GetAllCategories);

        if(response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<List<Category>>();

            if(result is null)
                return new List<Category>();

            return result;
        }

        return new List<Category>();
    }
}
