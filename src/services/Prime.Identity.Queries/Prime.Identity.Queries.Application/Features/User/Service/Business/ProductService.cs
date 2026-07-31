using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Prime.Identity.Queries.Application.Abstractions.Auth;
using Prime.Identity.Queries.Application.Abstractions.Cache;
using Prime.Identity.Queries.Application.Features.Category;
using Prime.Identity.Queries.Application.Features.Product;
using Prime.Identity.Queries.Application.Features.Product.GetStoreProducts;
using Prime.Identity.Queries.Application.Features.Store.GetAllStores;
using Prime.Identity.Queries.Domain.Entities.Business;
using Prime.Identity.Queries.Domain.Entities.Enums;
using Prime.Identity.Queries.Domain.Specifications.Business;

namespace Prime.Identity.Queries.Application.Features.User.Service.Business;

public class ProductService(
    IReadRepository<Domain.Entities.Business.Product> productRepository,
    IReadRepository<Domain.Entities.Business.Category> categoryRepository,
    ICurrentUserService currentUserService,
    ICacheService cacheService) : IProductService
{
    private readonly IReadRepository<Domain.Entities.Business.Product> _productRepository = productRepository;
    private readonly IReadRepository<Domain.Entities.Business.Category> _categoryRepository = categoryRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly ICacheService _cacheService = cacheService;
    private const string baseUrl = "https://localhost:7104/";
    public async Task<Result<List<GetStoreProductsResponse>>> GetStoreProductsAsync(CancellationToken ct = default)
    {
        var userId = Guid.TryParse(_currentUserService.UserId,out var parsedUserId)
? parsedUserId : throw new InvalidOperationException("Invalid user ID");

        var spec = new GetStoreProductsByOwnerIdSpec(parsedUserId);

        var products = await _productRepository.ListAsync(spec,ct);

        if(products is null || !products.Any())
            return Result.Failure<List<GetStoreProductsResponse>>("No products found for the current user.");

        var response = products.Select(product => new GetStoreProductsResponse(
            product.Id,
            product.StoreId,
            product.CategoryId,
            product.Name,
            baseUrl + product.Image,
            product.Description,
            product.CreatedAt,
            product.ModifiedAt
        )).ToList();

        return Result.Success(response);
    }
    public async Task<Result<List<GetStoreProductsResponse>>> GetStoreProductsById(Guid storeId,CancellationToken ct = default)
    {

        var products = await _productRepository.ListAsync(new GetStoreProductsByStoreIdSpec(storeId),ct);

        if(products is null || !products.Any())
            return Result.Failure<List<GetStoreProductsResponse>>("No products found for the current user.");

        var response = products.Select(product => new GetStoreProductsResponse(
            product.Id,
            product.StoreId,
            product.CategoryId,
            product.Name,
            baseUrl + product.Image,
            product.Description,
            product.CreatedAt,
            product.ModifiedAt
        )).ToList();

        return Result.Success(response);
    }

    public async Task<Result<List<GetAllProductsResponse>>> GetCategorizedProducts(CancellationToken ct = default)
    {

        var cacheKey = $"_products_";

        var cached = await _cacheService.GetAsync<List<GetAllProductsResponse>>(cacheKey,ct);

        if(cached is not null)
            return cached;

        var products = await _productRepository.ListAsync(new GetAllProductsSpec(),ct);

        var response = products.Select(p => new GetAllProductsResponse(p.Category.Name,p.Id.ToString(),p.Name,baseUrl+p.Image)).ToList();

        await _cacheService.SetAsync(cacheKey,response,TimeSpan.FromMinutes(1),ct);

        return Result.Success(response);
    }

    public async Task<Result<GetProductByIdResponse>> GetProductById(Guid productId,CancellationToken ct = default)
    {
        var spec = new GetProductByIdSpec(productId);

        var response = await _productRepository.FirstOrDefaultAsync(spec,ct);

        if(response == null)
            return Result.Failure<GetProductByIdResponse>("No product found!");

        var similarProducts = await _productRepository.ListAsync(new GetSimilarProductsSpec(response.CategoryId,productId),ct);

        var result = new GetProductByIdResponse(response.Id,response.Name,baseUrl + response.Image,response.Description,response.UnitPrice,response.CreatedAt,response.ModifiedAt,
            new Store.GetOwnerStore.GetOwnerStoreResponse(response.Store.Id,response.Store.UserId,response.Store.CategoryId,response.Store.Name,"","",response.Store.Description,response.Store.Address,response.Store.IsShippingAvailable,response.Store.City,response.Store.StoreStatus,response.Store.CreatedAt,response.Store.ModifiedAt),
            similarProducts.Select(product => new GetStoreProductsResponse(
                        product.Id,
                        product.StoreId,
                        product.CategoryId,
                        product.Name,
                        baseUrl + product.Image,
                        product.Description,
                        product.CreatedAt,
                        product.ModifiedAt
                    )).ToList());

        return Result.Success(result);
    }
}
