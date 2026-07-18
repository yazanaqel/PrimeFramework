using Application.Abstractions.Messaging;
using CSharpFunctionalExtensions;
using MediatR;
using Prime.Identity.Application.Abstractions;
using Prime.Identity.Application.Abstractions.Auth;
using Prime.Identity.Application.Features.Product.Create;
using Prime.Identity.Domain.Entities.Orders;
using Prime.Identity.Domain.Entities.Users;
using Prime.Identity.Domain.Specifications.Business;
using System.Text.RegularExpressions;

namespace Prime.Identity.Application.Features.Order.Create;

internal sealed class CreateOrderCommandHandler(
    IRepository<Domain.Entities.Products.Product> productRepository,
    IRepository<Domain.Entities.Stores.Store> storeRepository,
    IRepository<Domain.Entities.Orders.Order> orderRepository,
    IRepository<Domain.Entities.Orders.OrderItem> orderItemRepository,
    ICurrentUserService currentUserService) : ICommandHandler<CreateOrderCommand,bool>
{
    private readonly IRepository<Domain.Entities.Products.Product> _productRepository = productRepository;
    private readonly IRepository<Domain.Entities.Stores.Store> _storeRepository = storeRepository;
    private readonly IRepository<Domain.Entities.Orders.Order> _orderRepository = orderRepository;
    private readonly IRepository<Domain.Entities.Orders.OrderItem> _orderItemRepository = orderItemRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Result<bool>> Handle(CreateOrderCommand command,CancellationToken ct)
    {

        var userId = UserId.TryParse(_currentUserService.UserId,out var parsedUserId)
    ? parsedUserId : throw new InvalidOperationException("Invalid user ID");


        var spec = new ProductsByIdsSpec(command.Request.ProductIds);

        List<Domain.Entities.Products.Product> products =
            await _productRepository.ListAsync(spec);

        var result = products
            .GroupBy(p => p.Store)
            .Select(g => new
            {
                Store = g.Key,
                Products = g.ToList()
            })
            .ToList();


        var orders = new List<Domain.Entities.Orders.Order>();

        // Need optimization here

        foreach(var group in result)
        {

            Domain.Entities.Stores.Store store = group.Store;

            List<Domain.Entities.Products.Product> storeProducts = group.Products;

            Domain.Entities.Orders.Order order = Domain.Entities.Orders.Order.Create(store.Id,userId);

            await _orderRepository.AddAsync(order,ct);

            List<OrderItem> orderItems = new List<OrderItem>();

            foreach(var product in storeProducts)
            {
                var orderItem = OrderItem.Create(product.Id,order.Id,product.UnitPrice,command.Request.Quantity);

                orderItems.Add(orderItem);
            }

            await _orderItemRepository.AddRangeAsync(orderItems,ct);
        }


        return Result.Success(true);
    }

}