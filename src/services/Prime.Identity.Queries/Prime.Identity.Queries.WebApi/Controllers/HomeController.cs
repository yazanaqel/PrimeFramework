using Application.Features.User.GetAllUsers;
using Application.Features.User.GetUserById;
using Domain.Abstractions;
using Domain.Entities.User;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController(ReadOnlyDbContext applicationDbContext,IReadRepository<AppUser> readRepository) : Controller
{
    private readonly ReadOnlyDbContext _applicationDbContext = applicationDbContext;
    private readonly IReadRepository<AppUser> _readRepository = readRepository;

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
    public async Task<IActionResult> GetUserById(string userId,CancellationToken ct)
    {
        if(Guid.TryParse(userId,out Guid parsedGuid))
        {
            var queryHandler = new GetUserByIdQueryHandler(_readRepository);

            var response = await queryHandler.Handle(new GetUserByIdRequest(parsedGuid),ct);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        return BadRequest(new { valid = false,message = "Invalid GUID format." });
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersRequest request,CancellationToken ct)
    {
        var queryHandler = new GetAllUsersQueryHandler(_readRepository);

        var response = await queryHandler.Handle(request,ct);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }
}
