using Infrastructure.Authentication;
using Infrastructure.Authentication.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpGet("GetAll")]
    public IEnumerable<WeatherForecast> GetAll()
        => GetAllWeatherForecast();


    [Authorize(Roles = nameof(Roles.User))]
    [HttpGet("RoleUser")]
    public IEnumerable<WeatherForecast> RoleUser()
        => GetAllWeatherForecast();



    [Authorize(Policy = nameof(Permissions.Read))]
    [HttpGet("PolicyRead")]
    public IEnumerable<WeatherForecast> PolicyRead()
        => GetAllWeatherForecast();


    [HasPermission(Permissions.Modify)]
    [HttpGet("PermissionModify")]
    public IEnumerable<WeatherForecast> PermissionModify()
        => GetAllWeatherForecast();



    private IEnumerable<WeatherForecast> GetAllWeatherForecast()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();
    }
}
