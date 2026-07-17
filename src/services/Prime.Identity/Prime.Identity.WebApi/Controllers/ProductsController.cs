using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prime.Identity.Application.Features.Product.Create;
using WebApi.Constants;

namespace Prime.Identity.WebApi.Controllers;

[Authorize(Roles = nameof(Roles.MERCHANT))]
[Route("api/[controller]")]
[ApiController]
public class ProductsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequest request,CancellationToken ct)
    {
        var response = await _mediator.Send(new CreateProductCommand(request,ct));

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }



}
