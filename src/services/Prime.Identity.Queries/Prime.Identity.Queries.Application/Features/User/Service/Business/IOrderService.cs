using CSharpFunctionalExtensions;
using Prime.Identity.Queries.Application.Features.Order;

namespace Prime.Identity.Queries.Application.Features.User.Service.Business;

public interface IOrderService
{
    Task<Result<List<GetStoreOrdersResponse>>> GetStoreOrders(CancellationToken ct = default);
    Task<Result<List<GetUserOrdersResponse>>> GetUserOrders(CancellationToken ct = default);
}
