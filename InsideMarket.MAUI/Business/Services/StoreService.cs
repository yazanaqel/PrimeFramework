using InsideMarket.MAUI.Business.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static InsideMarket.MAUI.Route.RouteGate;

namespace InsideMarket.MAUI.Business.Services;
public class StoreService(IHttpClientFactory httpClientFactory) : IStoreService
{
    private readonly HttpClient _httpRead = httpClientFactory.CreateClient("Read");
    private readonly HttpClient _httpWrite = httpClientFactory.CreateClient("Write");

    public async Task<bool> CreateStore(Store store)
    {

        using var form = new MultipartFormDataContent();

        // Add normal fields
        form.Add(new StringContent(store.Name),"Name");
        form.Add(new StringContent(store.Description),"Description");
        form.Add(new StringContent(store.Address),"Address");
        form.Add(new StringContent(store.CategoryId),"CategoryId");
        form.Add(new StringContent(store.IsShippingAvailable.ToString()),"IsShippingAvailable");
        form.Add(new StringContent(((int)store.City).ToString()),"City");

        //Add files
        var fileContent = new StreamContent(store.ImageStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/*");
        form.Add(fileContent,"Image",store.ImageFileName);

        var fileCoverContent = new StreamContent(store.ImageCoverStream);
        fileCoverContent.Headers.ContentType = new MediaTypeHeaderValue("image/*");
        form.Add(fileCoverContent,"ImageCover",store.ImageCoverFileName);

        var response = await _httpWrite.PostAsync(StoreRouteGate.CreateStore,form);
        response.EnsureSuccessStatusCode();


        if(!response.IsSuccessStatusCode)
            return false;

        else
            return true;
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

    public async Task<Store> GetStore(Guid? storeId)
    {

        var url = storeId is null
    ? $"{StoreRouteGate.GetOwnerStore}"
    : $"{StoreRouteGate.GetStoreById}/{storeId}";


        var response = await _httpRead.GetAsync(url);

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
