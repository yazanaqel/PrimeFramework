using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Constants;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WeatherForecastController : ControllerBase
{
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