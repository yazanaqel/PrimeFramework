using CSharpFunctionalExtensions;
using Domain.Abstractions;
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
    ICurrentUserService currentUserService) : IOrderService
{
    private readonly IReadRepository<Domain.Entities.Business.Order> _orderRepository = orderRepository;
    private readonly IReadRepository<Domain.Entities.Business.Store> _storeRepository = storeRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Result<List<GetStoreOrdersResponse>>> GetStoreOrders(CancellationToken ct = default)
    {
        var userId = Guid.TryParse(_currentUserService.UserId,out var parsedUserId)
? parsedUserId : throw new InvalidOperationException("Invalid user ID");

        var spec1 = new GetOwnerStoreByIdSpec(parsedUserId);
        var store = await _storeRepository.FirstOrDefaultAsync(spec1,ct);

        var spec2 = new GetStoreOrdersSpec(store.Id);
        var orders = await _orderRepository.ListAsync(spec2, ct);

        var responses = orders.Select(o =>
        new GetStoreOrdersResponse(
            o.UserId.ToString() ?? "Unknown",string.Join(", ",
            o.OrderItems.Select(oi => oi.Product?.Name ?? "Unknown"))))
            .ToList();

        return Result.Success(responses);
    }

    public async Task<Result<List<GetUserOrdersResponse>>> GetUserOrders(CancellationToken ct = default)
    {
        var userId = Guid.TryParse(_currentUserService.UserId,out var parsedUserId)
? parsedUserId : throw new InvalidOperationException("Invalid user ID");

        var spec = new GetUserOrdersSpec(userId);

        var orders = await _orderRepository.ListAsync(spec, ct);

        var responses = orders.Select(o => 
        new GetUserOrdersResponse(
            o.Store?.Name ?? "Unknown", string.Join(", ",
            o.OrderItems.Select(oi => oi.Product?.Name ?? "Unknown"))))
            .ToList();

        return Result.Success(responses);
    }
}
