using Infrastructure;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Constants;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WeatherForecastController(ApplicationDbContext applicationDbContext) : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;


    [AllowAnonymous]
    [HttpPost("Insert")]
    public async Task Insert()
    {
        var hasher = new PasswordHasher<IdentityUser>();
        var user = new IdentityUser();
        var hash = hasher.HashPassword(user,"YourPassword123!");


        for (int i = 0; i < 999_999; i++)
        {

            Guid guid = Guid.NewGuid();

            GeneratedData data = Generator.Generate();

            string userName = data.FirstName+"."+ data.LastName;

            string email = data.Email;

            await _applicationDbContext.Database.ExecuteSqlRawAsync(@$"INSERT INTO [Identity].[Users] (
    Id,
    RefreshToken,
    CreatedOnUtc,
    UserName,
    NormalizedUserName,
    Email,
    NormalizedEmail,
    EmailConfirmed,
    PasswordHash,
    SecurityStamp,
    ConcurrencyStamp,
    PhoneNumberConfirmed,
    TwoFactorEnabled,
    LockoutEnabled,
    AccessFailedCount
)
VALUES (
    '{guid}',
    'AQAAAAIAAYagAAAAEJx0uYt3m5r1yQ5gYk1xq2Zp4u2GQ0xkq5x0zFJ6tq7o6k==',
    SYSUTCDATETIME(),
    '{userName}',
    'TEST',
    '{email}',
    'TEST@EXAMPLE.COM',
    1,
    'AQAAAAIAAYagAAAAEJx0uYt3m5r1yQ5gYk1xq2Zp4u2GQ0xkq5x0zFJ6tq7o6k==',
    'SEC123STAMP',
    'CONC123STAMP',
    0,
    0,
    1,
    0
)");


        }

    }


    [AllowAnonymous]
    [HttpGet("GetAll")]
    public IEnumerable<WeatherForecast> GetAll()
        => GetAllWeatherForecast();


    [Authorize(Roles = nameof(Roles.USER))]
    [HttpGet("RoleUser")]
    public IEnumerable<WeatherForecast> RoleUser()
        => GetAllWeatherForecast();


    [Authorize(Roles = nameof(Roles.ADMIN))]
    [HttpGet("RoleAdmin")]
    public IEnumerable<WeatherForecast> RoleAdmin()
    => GetAllWeatherForecast();


    [Authorize(Policy = nameof(Permissions.READ),Roles = "po")]
    [HttpGet("PolicyRead")]
    public IEnumerable<WeatherForecast> PolicyRead()
        => GetAllWeatherForecast();


    [HasPermission(Infrastructure.Authentication.Enums.Permissions.MODIFY)]
    [HttpGet("PermissionModify")]
    public IEnumerable<WeatherForecast> PermissionModify()
        => GetAllWeatherForecast();







    private IEnumerable<WeatherForecast> GetAllWeatherForecast()
    {
        return Enumerable.Range(1,5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20,55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();
    }
    private static readonly string[] Summaries = new[]{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
}
public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}

public record GeneratedData(string Email,string FirstName,string LastName);

public static class Generator
{
    private static readonly Random _random = new Random();

    private static readonly string[] FirstNames =
    {
        "Liam", "Noah", "Oliver", "Elijah", "James",
        "Emma", "Olivia", "Ava", "Sophia", "Isabella"
    };

    private static readonly string[] LastNames =
    {
        "Smith", "Johnson", "Williams", "Brown", "Jones",
        "Garcia", "Miller", "Davis", "Rodriguez", "Martinez"
    };

    private static readonly string[] Domains =
    {
        "gmail.com", "outlook.com", "yahoo.com", "hotmail.com", "example.com"
    };

    public static GeneratedData Generate()
    {
        string first = FirstNames[_random.Next(FirstNames.Length)];
        string last = LastNames[_random.Next(LastNames.Length)];
        string domain = Domains[_random.Next(Domains.Length)];

        int number = _random.Next(10,9999);

        string localPart = $"{first}.{last}{number}".ToLower();
        string email = $"{localPart}@{domain}";

        return new GeneratedData(email,first,last);
    }
}
