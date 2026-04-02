using Application.Features.User.GetAllUsers;
using Application.Features.User.GetUserById;
using Domain.Entities.User;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController(ReadOnlyDbContext applicationDbContext,IMediator mediator) : Controller
{
    private readonly ReadOnlyDbContext _applicationDbContext = applicationDbContext;
    private readonly IMediator _mediator = mediator;

    //[HttpGet("GetAllUsersAsync")]
    //public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
    //{

    //    return await _applicationDbContext
    //        .Set<AppUser>()
    //        .AsNoTracking()
    //        //.Where(u => u.UserName.Contains("yaza"))
    //        .ToListAsync();

    //}

    //[HttpGet("GetAllUsersYield")]
    //public async IAsyncEnumerable<AppUser> GetAllUsersYield()
    //{
    //    await foreach(var user in _applicationDbContext
    //            .Set<AppUser>()
    //            .AsNoTracking()
    //            .Where(u => u.UserName.Contains("yaza"))
    //            .AsAsyncEnumerable())
    //    {
    //        yield return user;
    //    }
    //}


    [HttpGet("GetUserById/{userId}")]
    public async Task<IActionResult> GetUserById(string userId, CancellationToken cancellationToken)
    {
        if(Guid.TryParse(userId,out Guid parsedGuid))
        {
            var response = await _mediator.Send(new GetUserByIdQuery(parsedGuid,cancellationToken));

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        return BadRequest(new { valid = false,message = "Invalid GUID format." });
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest request, CancellationToken ct)
    {
        var response = await _mediator.Send(new GetAllUsersQuery(request,ct));

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }
}
