using Microsoft.AspNetCore.Mvc;
using Prime.Identity.Queries.Application.Features.User.Service.Business;

namespace Prime.Identity.Queries.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet("GetAllCategories")]
    public async Task<IActionResult> GetAllCategories(CancellationToken ct)
    {
        var response = await _categoryService.GetAllCategoriesAsync(ct);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

}
