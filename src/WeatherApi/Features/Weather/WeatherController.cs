using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Net;

namespace WeatherApi.Features.Weather
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly WeatherService _weatherService;

        public WeatherController(ILogger<WeatherController> logger, WeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet()]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IReadOnlyList<WeatherDto>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(ProblemDetails))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllAsync(CancellationToken token = default)
        {
            var result = await _weatherService.GetAllAsync(token);

            return result.Match(
                result => Ok(),
                IActionResult (error) => error switch
                {
                    CancellationError => BadRequest(),
                    NotFoundError => NotFound(),
                    _ => Problem()
                });
        }

        [HttpGet("{id:int}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(WeatherDto))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(ProblemDetails))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(ProblemDetails))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken token = default)
        {
            var result = await _weatherService.GetByIdAsync(id, token);

            return result.Match(
                result => Ok(),
                IActionResult (error) => error switch
                {
                    CancellationError => BadRequest(),
                    NotFoundError => NotFound(),
                    _ => Problem()
                });
        }

        [HttpDelete("{id:int}")]
        [SwaggerDefaultResponse]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(ProblemDetails))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken token = default)
        {
            var result = await _weatherService.DeleteAsync(id, token);

            return result.Match(
                result => Ok(),
                IActionResult (error) => error switch
                {
                    CancellationError => BadRequest(),
                    NotFoundError => NotFound(),
                    _ => Problem()
                });
        }

        /// <summary>
        /// Create.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut()]
        [SwaggerResponse(HttpStatusCode.OK, typeof(WeatherDto))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(ProblemDetails))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(ProblemDetails))]
        public async Task<IActionResult> PutAsync([FromBody] WeatherDto dto, CancellationToken token = default)
        {
            var result = await _weatherService.CreateAsync(dto, token);

            return result.Match(
                result => Ok(),
                IActionResult (error) => error switch
                {
                    CancellationError => BadRequest(),
                    InvalidOperationError => BadRequest(),
                    NotFoundError => NotFound(),
                    _ => Problem()
                });
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("{id:int}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(WeatherDto))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(ProblemDetails))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(ProblemDetails))]
        public async Task<IActionResult> PostAsync([FromRoute] int id, WeatherDto dto, CancellationToken token = default)
        {
            var result = await _weatherService.UpdateAsync(id, dto, token);

            return result.Match(
                result => Ok(),
                IActionResult (error) => error switch
                {
                    CancellationError => BadRequest(),
                    InvalidOperationError => BadRequest(),
                    NotFoundError => NotFound(),
                    _ => Problem()
                });
        }
    }
}