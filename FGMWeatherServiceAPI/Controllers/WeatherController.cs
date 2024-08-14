using FGMWeatherServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FGMWeatherServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        /// <summary>
        /// Retrieves the current weather data based on the provided geographic coordinates.
        /// </summary>
        /// <param name="latitude">The latitude of the location to get weather data for.</param>
        /// <param name="longitude">The longitude of the location to get weather data for.</param>
        /// <returns>Returns a JSON object containing the current temperature, wind speed, wind direction, and sunrise time for the specified location.</returns>
        /// <response code="200">Returns the weather data for the specified location.</response>
        /// <response code="400">If the latitude or longitude is invalid or missing.</response>
        /// <response code="404">If no weather data is found for the specified location.</response>
        [HttpGet("location")]
        public async Task<IActionResult> GetWeatherByLocation([FromQuery] double latitude, [FromQuery] double longitude)
        {
            var weatherData = await _weatherService.GetWeatherByLocationAsync(latitude, longitude);
            return Ok(weatherData);
        }

        /// <summary>
        /// Retrieves the current weather data based on the provided city name.
        /// </summary>
        /// <param name="city">The name of the city to get weather data for.</param>
        /// <returns>Returns a JSON object containing the current temperature, wind speed, wind direction, and sunrise time for the specified city.</returns>
        /// <response code="200">Returns the weather data for the specified city.</response>
        /// <response code="400">If the city name is invalid or missing.</response>
        /// <response code="404">If no weather data is found for the specified city.</response>
        [HttpGet("city")]
        public async Task<IActionResult> GetWeatherByCity([FromQuery] string city)
        {
            var weatherData = await _weatherService.GetWeatherByCityAsync(city);
            return Ok(weatherData);
        }
    }
}
