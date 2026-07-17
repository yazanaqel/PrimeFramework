using Application.Abstractions.Messaging;
using CSharpFunctionalExtensions;
using Prime.Identity.Application.Abstractions;
using Prime.Identity.Application.Abstractions.Auth;
using Prime.Identity.Domain.Entities.Users;
using Prime.Identity.Domain.Specifications.Business;

namespace Prime.Identity.Application.Features.Product.Create;

internal sealed class CreateProductCommandHandler(
    IRepository<Domain.Entities.Products.Product> productRepository,
    IRepository<Domain.Entities.Stores.Store> storeRepository,
    ICurrentUserService currentUserService,
    IImageService imageService) : ICommandHandler<CreateProductCommand,bool>
{
    private readonly IRepository<Domain.Entities.Products.Product> _productRepository = productRepository;
    private readonly IRepository<Domain.Entities.Stores.Store> _storeRepository = storeRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IImageService _imageService = imageService;

    public async Task<Result<bool>> Handle(CreateProductCommand command,CancellationToken ct)
    {

        var userId = UserId.TryParse(_currentUserService.UserId,out var parsedUserId)
    ? parsedUserId : throw new InvalidOperationException("Invalid user ID");


        var spec = new GetOwnerStoreByIdSpecification(parsedUserId);

        var store = await _storeRepository.FirstOrDefaultAsync(spec,ct);

        if(store == null)
            throw new InvalidOperationException("Invalid user ID");

        string imagePath = string.Empty;

        if(command.Request.ImageFile is not null)
            imagePath = await _imageService.SaveImageAsync(command.Request.ImageFile);

        var product = Domain.Entities.Products.Product.Create(
            store.Id,
            command.Request.CategoryId,
            command.Request.Name,
            command.Request.Description,
            imagePath,
            command.Request.StockQuantity,
            command.Request.UnitPrice);

        await _productRepository.AddAsync(product,ct);

        return Result.Success(true);
    }

}