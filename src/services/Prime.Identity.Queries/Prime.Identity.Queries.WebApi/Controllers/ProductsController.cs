using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prime.Identity.Queries.Application.Features.User.Service.Business;

namespace Prime.Identity.Queries.WebApi.Controllers;

[Authorize(Roles = "MERCHANT")]
[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet("GetStoreProducts")]
    public async Task<IActionResult> GetStoreProducts(CancellationToken ct)
    {
        var response = await _productService.GetStoreProductsAsync(ct);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [AllowAnonymous]
    [HttpGet("GetStoreProductsById/{storeId:guid}")]
    public async Task<IActionResult> GetStoreProductsById(Guid storeId,CancellationToken ct)
    {
        var response = await _productService.GetStoreProductsById(storeId,ct);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [AllowAnonymous]
    [HttpGet("GetCategorizedProducts")]
    public async Task<IActionResult> GetCategorizedProducts(CancellationToken ct)
    {
        var response = await _productService.GetCategorizedProducts(ct);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [AllowAnonymous]
    [HttpGet("GetProductById/{productId:guid}")]
    public async Task<IActionResult> GetProductById(Guid productId,CancellationToken ct)
    {
        var response = await _productService.GetProductById(productId,ct);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }
}
