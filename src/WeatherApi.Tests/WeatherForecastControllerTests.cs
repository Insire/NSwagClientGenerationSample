using Microsoft.AspNetCore.Mvc.Testing;
using WeatherApi.Client;

namespace WeatherApi.Tests
{
    public class WeatherForecastControllerTests : IClassFixture<WebApplicationFactory<WeatherForecast>>
    {
        private readonly WebApplicationFactory<WeatherForecast> _factory;
        private readonly WeatherForecastClient _client;

        public WeatherForecastControllerTests(WebApplicationFactory<WeatherForecast> factory)
        {
            _factory = factory;

            var httpClient = _factory.CreateClient();
            _client = new WeatherForecastClient(httpClient);
        }

        [Fact]
        public async Task GetWeatherForecastAsync_Should_Return_Expected_Result()
        {
            // Act
            var response = await _client.GetWeatherForecastAsync();

            // Assert
            Assert.True(response is not null);
            Assert.True(response.Result.Count > 0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task GetWeatherForecastByIdAsync_Should_Return_Expected_Result(int id)
        {
            // Act
            var response = await _client.GetWeatherForecastByIdAsync(id);

            // Assert
            Assert.True(response is not null);
            Assert.True(response.Result is not null);
            Assert.True(response.Result.Date is not null);
            Assert.True(response.Result.TemperatureF is not null);
            Assert.True(response.Result.Summary is not null);
        }
    }
}