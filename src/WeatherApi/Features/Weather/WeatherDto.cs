using System.ComponentModel.DataAnnotations;

namespace WeatherApi.Features.Weather;

public sealed class WeatherDto
{
    [Required]
    required public DateOnly Date { get; init; }

    [Required]
    [Range(-275, 9000)]
    required public int TemperatureC { get; init; }

    [Required]
    [StringLength(256, MinimumLength = 1)]
    required public string? Summary { get; init; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}