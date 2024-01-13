using System.Text.Json;

namespace WeatherApi.Client
{
    public abstract class ClientBase
    {
        protected static void UpdateJsonSerializerSettings(JsonSerializerOptions options)
        {
            options.TypeInfoResolver = SourceGenerationContext.Default;
            options.PropertyNameCaseInsensitive = true;
        }
    }
}
