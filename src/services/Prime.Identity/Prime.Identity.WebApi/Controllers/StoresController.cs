using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prime.Identity.Application.Features.Store.ChangeStoreStatus;
using Prime.Identity.Application.Features.Store.Create;
using WebApi.Constants;

namespace Prime.Identity.WebApi.Controllers;

[Authorize(Roles = nameof(Roles.MERCHANT))]
[Route("api/[controller]")]
[ApiController]
public class StoresController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpPost("CreateStore")]
    public async Task<IActionResult> CreateStore([FromForm] CreateStoreRequest request,CancellationToken ct)
    {
        var response = await _mediator.Send(new CreateStoreCommand(request,ct));

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    [Authorize(Roles = nameof(Roles.ADMIN))]
    [HttpPost("ChangeStoreStatus")]
    public async Task<IActionResult> ChangeStoreStatus(ChangeStoreStatusRequest request,CancellationToken ct)
    {
        var response = await _mediator.Send(new ChangeStoreStatusCommand(request,ct));

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }
}