using FGMWeatherServiceAPI.Models;

namespace FGMWeatherServiceAPI.Services
{
    /// <summary>
    /// Provides methods to retrieve weather data.
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Retrieves weather data based on geographical location.
        /// </summary>
        /// <param name="latitude">The latitude of the location.</param>
        /// <param name="longitude">The longitude of the location.</param>
        /// <returns>A <see cref="Task{WeatherData}"/> representing the asynchronous operation. 
        /// The task result contains the <see cref="WeatherData"/> for the specified location.</returns>
        Task<WeatherData> GetWeatherByLocationAsync(double latitude, double longitude);

        /// <summary>
        /// Retrieves weather data based on the city name.
        /// </summary>
        /// <param name="city">The name of the city.</param>
        /// <returns>A <see cref="Task{WeatherData}"/> representing the asynchronous operation. 
        /// The task result contains the <see cref="WeatherData"/> for the specified city.</returns>
        Task<WeatherData> GetWeatherByCityAsync(string city);
    }
}