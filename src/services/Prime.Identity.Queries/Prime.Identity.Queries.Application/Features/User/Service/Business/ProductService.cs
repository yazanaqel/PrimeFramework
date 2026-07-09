using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Prime.Identity.Queries.Application.Abstractions.Auth;
using Prime.Identity.Queries.Application.Features.Product.GetStoreProducts;
using Prime.Identity.Queries.Domain.Specifications.Business;

namespace Prime.Identity.Queries.Application.Features.User.Service.Business;

public class ProductService(IReadRepository<Domain.Entities.Business.Product> productRepository,ICurrentUserService currentUserService) : IProductService
{
    private readonly IReadRepository<Domain.Entities.Business.Product> _productRepository = productRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Result<List<GetStoreProductsResponse>>> GetStoreProductsAsync(CancellationToken ct = default)
    {
        var userId = Guid.TryParse(_currentUserService.UserId,out var parsedUserId)
? parsedUserId : throw new InvalidOperationException("Invalid user ID");

        var spec = new GetStoreProductsByOwnerIdSpecification(parsedUserId);

        var products = await _productRepository.ListAsync(spec,ct);

        if(products is null || !products.Any())
            return Result.Failure<List<GetStoreProductsResponse>>("No products found for the current user.");

        var response = products.Select(product => new GetStoreProductsResponse(
            product.Id,
            product.StoreId,
            product.CategoryId,
            product.Name,
            product.Image,
            product.Description,
            product.CreatedAt,
            product.ModifiedAt
        )).ToList();

        return Result.Success(response);
    }

}
