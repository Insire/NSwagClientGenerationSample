using Remora.Results;

namespace WeatherApi.Features.Weather
{
    public sealed class WeatherService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly Random _random;

        public WeatherService(Random random)
        {
            _random = random;
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if ((_random.Next() % 2) == 0) // imagine a db deletion taking place here
            {
                return new InvalidOperationError($"Weather with ID {id} does not exist.");
            }
            else
            {
                return Result.FromSuccess();
            }
        }

        public async Task<Result<IReadOnlyList<WeatherDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if ((_random.Next() % 2) == 0) // imagine a db deletion taking place here
            {
                return Array.Empty<WeatherDto>();
            }
            else
            {
                var count = _random.Next(1, 5);

                var results = Enumerable
                    .Range(1, count)
                    .Select(index => new WeatherDto
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = _random.Next(-20, 55),
                        Summary = Summaries[_random.Next(Summaries.Length)]
                    })
                    .ToArray();

                return results;
            }
        }

        public async Task<Result<WeatherDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new GenericError("Cancelled");
            }

            if ((_random.Next() % 2) == 0) // imagine a db deletion taking place here
            {
                return new NotFoundError();
            }
            else
            {
                var result = new WeatherDto
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(id)),
                    TemperatureC = _random.Next(-20, 55),
                    Summary = Summaries[_random.Next(Summaries.Length)]
                };

                return result;
            }
        }

        public async Task<Result<WeatherDto>> UpdateAsync(int id, WeatherDto dto, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new GenericError("Cancelled");
            }

            if ((_random.Next() % 2) == 0) // imagine a db deletion taking place here
            {
                return new NotFoundError();
            }
            else
            {
                if ((_random.Next() % 2) == 0)
                {
                    return new InvalidOperationError("Weather could not be updated");
                }
                else
                {
                    return dto;
                }
            }
        }

        public async Task<Result<WeatherDto>> CreateAsync(WeatherDto dto, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new GenericError("Cancelled");
            }

            if ((_random.Next() % 2) == 0) // imagine a db deletion taking place here
            {
                return new InvalidOperationError("Weather could not be created");
            }
            else
            {
                return dto;
            }
        }
    }
}