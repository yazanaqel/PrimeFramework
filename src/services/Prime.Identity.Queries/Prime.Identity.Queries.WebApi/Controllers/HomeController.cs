using Application.Features.User.GetAllUsers;
using Application.Features.User.GetUserById;
using Domain.Abstractions;
using Domain.Entities.User;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Prime.Identity.Queries.Application.Features.User.Service;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController(ReadOnlyDbContext applicationDbContext,IUserService userService) : Controller
{
    private readonly ReadOnlyDbContext _applicationDbContext = applicationDbContext;
    private readonly IUserService _userService = userService;

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
    public async Task<IActionResult> GetUserById([Required] string userId,CancellationToken ct)
    {
        if(Guid.TryParse(userId,out Guid parsedGuid))
        {
            var response = await _userService.GetUserByIdAsync(parsedGuid,ct);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        return BadRequest(new { valid = false,message = "Invalid GUID format." });
    }
    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersRequest request, CancellationToken ct)
    {
        var response = await _userService.GetAllUsersAsync(request,ct);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

}
