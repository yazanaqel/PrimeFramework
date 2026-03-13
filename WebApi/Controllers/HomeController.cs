using Domain.Entities.Users;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController(ApplicationDbContext applicationDbContext) : Controller
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

    [HttpGet("GetAllUsersAsync")]
    public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
    {

        return await _applicationDbContext
            .Set<AppUser>()
            .AsNoTracking()
            //.Where(u => u.UserName.Contains("yaza"))
            .ToListAsync();

    }

    [HttpGet("GetAllUsersYield")]
    public async IAsyncEnumerable<AppUser> GetAllUsersYield()
    {
        await foreach(var user in _applicationDbContext
                .Set<AppUser>()
                .AsNoTracking()
                .Where(u => u.UserName.Contains("yaza"))
                .AsAsyncEnumerable())
        {
            yield return user;
        }
    }



}
