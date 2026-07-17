using InsideMarket.MAUI.Business.Models;

namespace InsideMarket.MAUI.Business.Services;

public interface IBasketService
{
    Task SaveBasket(List<ProductInBasket> products);
    Task<List<ProductInBasket>> LoadBasket();
    Task ClearBasket();
}
