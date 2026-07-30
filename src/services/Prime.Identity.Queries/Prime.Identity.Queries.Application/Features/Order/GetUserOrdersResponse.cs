using Prime.Identity.Queries.Domain.Entities.Business;
using Prime.Identity.Queries.Domain.Entities.Enums;
using System.Xml.Linq;

namespace Prime.Identity.Queries.Application.Features.Order;

public record GetUserOrdersResponse(string StoreName,string ProductName,OrderItemStatus OrderItemStatus);
