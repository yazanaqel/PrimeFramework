using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prime.Identity.Queries.Application.Features.User.Service.Business;

namespace Prime.Identity.Queries.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;


    [HttpGet("GetUserOrders")]
    public async Task<IActionResult> GetUserOrders(CancellationToken ct)
    {
        var response = await _orderService.GetUserOrders(ct);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [Authorize(Roles = "MERCHANT")]
    [HttpGet("GetStoreOrders")]
    public async Task<IActionResult> GetStoreOrders(CancellationToken ct)
    {
        var response = await _orderService.GetStoreOrders(ct);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }
}
