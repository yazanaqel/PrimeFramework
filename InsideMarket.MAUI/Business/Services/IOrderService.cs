using InsideMarket.MAUI.Business.Models;

namespace InsideMarket.MAUI.Business.Services;

public interface IOrderService
{
    Task<bool> CreateOrder(List<CreateOrderRequest> orderItems);
    Task<IEnumerable<GetUserOrdersResponse>> GetUserOrders();
    Task<IEnumerable<GetStoreOrdersResponse>> GetStoreOrders();
}
