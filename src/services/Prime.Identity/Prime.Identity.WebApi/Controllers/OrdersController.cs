using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prime.Identity.Application.Features.Order.Create;
using WebApi.Constants;

namespace Prime.Identity.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrdersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpPost("CreateOrder")]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request,CancellationToken ct)
    {
        var response = await _mediator.Send(new CreateOrderCommand(request,ct));

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }
}
