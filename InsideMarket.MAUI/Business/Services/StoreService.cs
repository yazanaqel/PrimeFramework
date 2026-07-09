using InsideMarket.MAUI.Auth;
using InsideMarket.MAUI.Business.Models;
using System.Net.Http.Json;
using static InsideMarket.MAUI.Route.RouteGate;

namespace InsideMarket.MAUI.Business.Services;

public class StoreService(IHttpClientFactory httpClientFactory) : IStoreService
{
    private readonly HttpClient _httpRead = httpClientFactory.CreateClient("Read");
    private readonly HttpClient _httpWrite = httpClientFactory.CreateClient("Write");

    public async Task<bool> CreateStore(Store store)
    {
        var response = await _httpWrite.PostAsJsonAsync(StoreRouteGate.CreateStore,store);

        if(!response.IsSuccessStatusCode)
            return false;

        else return true;
    }

    public async Task<IEnumerable<Store>> GetAllStores(GetAllStoresRequest getAllStoresRequest)
    {
        var response = await _httpRead.GetAsync(StoreRouteGate.GetAllStores);

        if(response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Store>>();

            if(result is null)
                return new List<Store>();

            return result;
        }
        else
        {
            return null;
        }
    }

    public async Task<Store> GetOwnerStore()
    {
        var response = await _httpRead.GetAsync(StoreRouteGate.GetOwnerStore);

        if(response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<Store>();

            if(result is null)
                return new Store();

            return result;
        }
        else
        {
            return null;
        }

    }
}
