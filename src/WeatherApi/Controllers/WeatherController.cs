using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
[OpenApiTag("Weather")]
[ApiExplorerSettings(GroupName = "Weather")]
public class WeatherController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public IEnumerable<WeatherForecast> GetWeatherForecast()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("{id:int}")]
    public WeatherForecast GetWeatherForecastById([FromRoute] int id)
    {
        return new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(id)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        };
    }

    [HttpDelete("{id:int}")]
    public void DeleteWeatherForecast([FromRoute] int id)
    {

    }

    [HttpPut("{id:int}")]
    public void PutWeatherForecast([FromRoute] int id)
    {

    }

    [HttpPost("{id:int}")]
    public void PostWeatherForecast([FromRoute] int id)
    {

    }
}
