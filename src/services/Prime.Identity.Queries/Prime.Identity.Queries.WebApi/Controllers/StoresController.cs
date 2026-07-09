using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prime.Identity.Queries.Application.Features.Store.GetAllStores;
using Prime.Identity.Queries.Application.Features.User.Service.Business;

namespace Prime.Identity.Queries.WebApi.Controllers;

[Authorize(Roles = "MERCHANT")]
[Route("api/[controller]")]
[ApiController]
public class StoresController(IStoreService storeService) : ControllerBase
{
    private readonly IStoreService _storeService = storeService;

    [HttpGet("GetOwnerStore")]
    public async Task<IActionResult> GetOwnerStore(CancellationToken ct)
    {
        var response = await _storeService.GetOwnerStoreAsync(ct);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }


    [AllowAnonymous]
    [HttpGet("GetAllStores")]
    public async Task<IActionResult> GetAllStores([FromQuery] GetAllStoresRequest request,CancellationToken ct)
    {
        var response = await _storeService.GetAllStores(request,ct);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }
}
