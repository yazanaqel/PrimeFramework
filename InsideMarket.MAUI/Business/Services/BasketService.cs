using InsideMarket.MAUI.Business.Models;
using System.Text.Json;

namespace InsideMarket.MAUI.Business.Services;


public class BasketService : IBasketService
{
    private const string UserIdKey = "userId";
    public async Task SaveBasket(List<ProductInBasket> products)
    {
        var userId = Preferences.Get(UserIdKey,null);

        var json = JsonSerializer.Serialize(products);

        Preferences.Set($"basket_{userId}",json);

    }

    public async Task<List<ProductInBasket>> LoadBasket()
    {
        var userId = Preferences.Get(UserIdKey,null);
        var json = Preferences.Get($"basket_{userId}",string.Empty);
        return string.IsNullOrEmpty(json)
            ? new List<ProductInBasket>()
            : JsonSerializer.Deserialize<List<ProductInBasket>>(json);
    }
    public async Task ClearBasket()
    {
        var userId = Preferences.Get(UserIdKey,null);

        Preferences.Remove($"basket_{userId}");

    }

}
