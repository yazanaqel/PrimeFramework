using Application.Abstractions.Messaging;
using Ardalis.Specification;
using CSharpFunctionalExtensions;
using Prime.Identity.Application.Abstractions;
using Prime.Identity.Application.Abstractions.Auth;
using Prime.Identity.Domain.Entities.Users;
using Prime.Identity.Domain.Specifications.Business;

namespace Prime.Identity.Application.Features.Product.Create;

internal sealed class CreateProductCommandHandler(
    IRepository<Domain.Entities.Products.Product> productRepository,
    IRepository<Domain.Entities.Stores.Store> storeRepository,
    ICurrentUserService currentUserService) : ICommandHandler<CreateProductCommand,bool>
{
    private readonly IRepository<Domain.Entities.Products.Product> _productRepository = productRepository;
    private readonly IRepository<Domain.Entities.Stores.Store> _storeRepository = storeRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Result<bool>> Handle(CreateProductCommand command,CancellationToken ct)
    {

        var userId = UserId.TryParse(_currentUserService.UserId,out var parsedUserId)
    ? parsedUserId : throw new InvalidOperationException("Invalid user ID");



        var spec = new GetOwnerStoreByIdSpecification(parsedUserId);

        var store = await _storeRepository.FirstOrDefaultAsync(spec,ct);

        if(store == null)
            throw new InvalidOperationException("Invalid user ID");

        var product = Domain.Entities.Products.Product.Create(
            store.Id,
            command.Request.CategoryId,
            command.Request.Name,
            command.Request.Description,
            command.Request.Image,
            command.Request.StockQuantity,
            command.Request.UnitPrice);

        try
        {
            await _productRepository.AddAsync(product,ct);

        }
        catch(Exception ex) { Console.WriteLine(ex.InnerException); }
        return Result.Success(true);
    }

}