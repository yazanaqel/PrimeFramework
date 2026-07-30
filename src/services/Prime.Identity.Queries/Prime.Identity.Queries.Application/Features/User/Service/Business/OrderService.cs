using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Domain.Entities.User;
using Domain.Specifications.User;
using Prime.Identity.Queries.Application.Abstractions.Auth;
using Prime.Identity.Queries.Application.Features.Order;
using Prime.Identity.Queries.Domain.Specifications.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prime.Identity.Queries.Application.Features.User.Service.Business;

public class OrderService(
    IReadRepository<Domain.Entities.Business.Order> orderRepository,
    IReadRepository<Domain.Entities.Business.Store> storeRepository,
    IReadRepository<AppUser> userIdentity,
    ICurrentUserService currentUserService) : IOrderService
{
    private readonly IReadRepository<Domain.Entities.Business.Order> _orderRepository = orderRepository;
    private readonly IReadRepository<Domain.Entities.Business.Store> _storeRepository = storeRepository;
    private readonly IReadRepository<AppUser> _userIdentity = userIdentity;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Result<List<GetStoreOrdersResponse>>> GetStoreOrders(CancellationToken ct = default)
    {
        var userId = Guid.TryParse(_currentUserService.UserId,out var parsedUserId)
? parsedUserId : throw new InvalidOperationException("Invalid user ID");


        var store = await _storeRepository.FirstOrDefaultAsync(new GetOwnerStoreByIdSpec(parsedUserId),ct);

        var orders = await _orderRepository.ListAsync(new GetStoreOrdersSpec(store.Id),ct);

        var responses = orders.Select(o =>
        new GetStoreOrdersResponse(
            o.UserId.ToString() ?? "Unknown",
            o.Id.ToString(),
            string.Join(", ",
            o.OrderItems.Select(oi => oi.Product?.Name ?? "Unknown"))))
            .ToList();

        return Result.Success(responses);
    }

    public async Task<Result<List<GetUserOrdersResponse>>> GetUserOrders(CancellationToken ct = default)
    {
        var userId = Guid.TryParse(_currentUserService.UserId,out var parsedUserId)
? parsedUserId : throw new InvalidOperationException("Invalid user ID");

        var orders = await _orderRepository.ListAsync(new GetUserOrdersSpec(userId),ct);

        var responses = orders.Select(o => new GetUserOrdersResponse(o.Id.ToString(),o.Store.Name)).ToList();

        return Result.Success(responses);
    }


    public async Task<Result<GetOrderResponse>> GetOrderById(string orderId,CancellationToken ct = default)
    {
        var orderIdResult = Guid.TryParse(orderId,out var parsedOrderId)
? parsedOrderId : throw new InvalidOperationException("Invalid order ID");


        var order = await _orderRepository.FirstOrDefaultAsync(new GetOrderByIdSpec(orderIdResult),ct);

        var user = await _userIdentity.FirstOrDefaultAsync(new GetUserByIdSpecification(order.UserId),ct);


        var responses = new GetOrderResponse(order.Id.ToString(),user.Email,user.PhoneNumber,order.OrderItems.Select(o=> new OrderItemResponse(o.Id.ToString(),o.Product.Name,o.OrderItemStatus)));


        return Result.Success(responses);
    }
}
