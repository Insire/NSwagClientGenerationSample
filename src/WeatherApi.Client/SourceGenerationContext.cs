using System.Text.Json.Serialization;

namespace WeatherApi.Client
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(WeatherForecast))]
    [JsonSerializable(typeof(IReadOnlyList<WeatherForecast>))]
    internal partial class SourceGenerationContext : JsonSerializerContext
    {
    }
}
