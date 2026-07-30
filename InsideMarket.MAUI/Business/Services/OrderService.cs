using InsideMarket.MAUI.Business.Models;
using System.Net.Http.Json;
using static InsideMarket.MAUI.Route.RouteGate;

namespace InsideMarket.MAUI.Business.Services;

public class OrderService(IHttpClientFactory httpClientFactory) : IOrderService
{
    private readonly HttpClient _httpRead = httpClientFactory.CreateClient("Read");
    private readonly HttpClient _httpWrite = httpClientFactory.CreateClient("Write");
    public async Task<bool> CreateOrder(List<CreateOrderRequest> orderItems)
    {
        var response = await _httpWrite.PostAsJsonAsync(OrderRouteGate.CreateOrder,orderItems);

        response.EnsureSuccessStatusCode();

        if(!response.IsSuccessStatusCode)
            return false;

        return true;
    }

    public async Task<IEnumerable<GetUserOrdersResponse>> GetUserOrders()
    {
        var response = await _httpRead.GetAsync(OrderRouteGate.GetUserOrders);
        response.EnsureSuccessStatusCode();

        if(response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<GetUserOrdersResponse>>();

            if(result is null)
                return Enumerable.Empty<GetUserOrdersResponse>();

            return result;
        }
        else
        {
            return Enumerable.Empty<GetUserOrdersResponse>();
        }
    }

    public async Task<IEnumerable<GetStoreOrdersResponse>> GetStoreOrders()
    {
        var response = await _httpRead.GetAsync(OrderRouteGate.GetStoreOrders);
        response.EnsureSuccessStatusCode();

        if(response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<GetStoreOrdersResponse>>();

            if(result is null)
                return Enumerable.Empty<GetStoreOrdersResponse>();

            return result;
        }
        else
        {
            return Enumerable.Empty<GetStoreOrdersResponse>();
        }
    }

    public async Task<GetOrderResponse> GetOrderById(string orderId)
    {
        var response = await _httpRead.GetAsync($"{OrderRouteGate.GetOrderById}/{orderId}");
        response.EnsureSuccessStatusCode();

        if(response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<GetOrderResponse>();

            if(result is null)
                return new GetOrderResponse();

            return result;
        }
        else
        {
            return new GetOrderResponse();
        }
    }

    public async Task<bool> ChangeOrderItemStatus(List<ChangeOrderItemStatus> changeOrderItemStatus)
    {
        var response = await _httpWrite.PostAsJsonAsync(OrderRouteGate.ChangeOrderItemStatus,changeOrderItemStatus);

        response.EnsureSuccessStatusCode();

        if(!response.IsSuccessStatusCode)
            return false;

        return true;
    }
}
