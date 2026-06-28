using Infrastructure.Authentication.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prime.Identity.Application.Abstractions.Auth;
using Prime.Identity.Application.Features.Store.Create;
using Prime.Identity.Domain.Entities.Users;
using System.Security.Claims;

namespace Prime.Identity.WebApi.Controllers;

[Authorize(Roles = nameof(Roles.MERCHANT))]
[Route("api/[controller]")]
[ApiController]
public class StoresController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpPost("CreateStore")]
    public async Task<IActionResult> CreateStore([FromBody] CreateStoreRequest request,CancellationToken ct)
    {
        var response = await _mediator.Send(new CreateStoreCommand(request,ct));

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }



}
