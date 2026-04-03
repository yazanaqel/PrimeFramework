using Infrastructure;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Constants;

namespace WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class WeatherForecastController(ApplicationDbContext applicationDbContext) : ControllerBase
{

    [AllowAnonymous]
    [HttpGet("AllowAnonymous")]
    public IEnumerable<WeatherForecast> AllowAnonymous()
        => GetAllWeatherForecast();

    [HttpGet("Authorize")]
    public IEnumerable<WeatherForecast> Authorize()
        => GetAllWeatherForecast();

    [Authorize(Roles = nameof(Roles.ADMIN))]
    [HttpGet("AdminRole")]
    public IEnumerable<WeatherForecast> AdminRole()
    => GetAllWeatherForecast();

    [Authorize(Policy = nameof(Permissions.READ))]
    [HttpGet("ReadPermission")]
    public IEnumerable<WeatherForecast> ReadPermission()
        => GetAllWeatherForecast();

    [Authorize(Roles = nameof(Roles.ADMIN), Policy = nameof(Permissions.READ))]
    [HttpGet("AdminRoleAndReadPermission")]
    public IEnumerable<WeatherForecast> AdminRoleAndReadPermission()
    => GetAllWeatherForecast();


    //[HasPermission(Infrastructure.Authentication.Enums.Permissions.MODIFY)]
    //[HttpGet("PermissionModify")]
    //public IEnumerable<WeatherForecast> PermissionModify()
    //    => GetAllWeatherForecast();







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
