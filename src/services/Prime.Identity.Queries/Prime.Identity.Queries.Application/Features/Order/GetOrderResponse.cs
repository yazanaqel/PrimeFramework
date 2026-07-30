using Prime.Identity.Queries.Domain.Entities.Business;

namespace Prime.Identity.Queries.Application.Features.Order;

public record GetOrderResponse(string OrderId,string Email,string PhoneNumber,IEnumerable<OrderItemResponse> OrderItems);
