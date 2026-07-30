using Application.Features.User.GetUserById;
using Prime.Identity.Queries.Application.Features.Product.GetStoreProducts;

namespace Prime.Identity.Queries.Application.Features.Order;

public record GetStoreOrdersResponse(string UserId,string OrderId,string ProductName);
