using FGMWeatherServiceAPI.Configurations;
using FGMWeatherServiceAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace FGMWeatherServiceAPI.Services
{
    /// <summary>
    /// Service to retrieve and manage weather data.
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IMongoCollection<WeatherData> _weatherCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client used for making API requests.</param>
        /// <param name="mongoClient">The MongoDB client used to interact with the database.</param>
        /// <param name="mongoDbSettings">The MongoDB settings configuration.</param>
        public WeatherService(HttpClient httpClient, IMongoClient mongoClient, IOptions<MongoDbSettings> mongoDbSettings)
        {
            _httpClient = httpClient;
            var database = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _weatherCollection = database.GetCollection<WeatherData>(mongoDbSettings.Value.CollectionName);
        }

        /// <summary>
        /// Retrieves weather data based on geographical location (latitude and longitude).
        /// </summary>
        /// <param name="latitude">The latitude of the location.</param>
        /// <param name="longitude">The longitude of the location.</param>
        /// <returns>A <see cref="Task{WeatherData}"/> representing the asynchronous operation.
        /// The task result contains the <see cref="WeatherData"/> for the specified location.</returns>
        /// <exception cref="ArgumentException">Thrown if latitude or longitude is out of range.</exception>
        public async Task<WeatherData> GetWeatherByLocationAsync(double latitude, double longitude)
        {
            // Validate latitude and longitude
            if (latitude < -90 || latitude > 90)
            {
                throw new ArgumentException("Latitude must be between -90 and 90 degrees.");
            }

            if (longitude < -180 || longitude > 180)
            {
                throw new ArgumentException("Longitude must be between -180 and 180 degrees.");
            }

            var weatherData = await _weatherCollection
                .Find(w => w.Latitude == latitude && w.Longitude == longitude)
                .FirstOrDefaultAsync();
            // If weatherData exists in DB, retrieve the info to avoid making a second call to the API
            if (weatherData == null)
            {
                var response = await _httpClient.GetStringAsync(
                    $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true&daily=sunrise&timezone=auto");

                var json = JObject.Parse(response);

                weatherData = new WeatherData
                {
                    Latitude = latitude,
                    Longitude = longitude,
                    Temperature = (double)json["current_weather"]["temperature"],
                    WindSpeed = (double)json["current_weather"]["windspeed"],
                    WindDirection = (int)json["current_weather"]["winddirection"],
                    Sunrise = DateTime.ParseExact((string)json["daily"]["sunrise"][0], "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture)
                };
                await _weatherCollection.InsertOneAsync(weatherData);
            }
            return weatherData;
        }

        /// <summary>
        /// Retrieves weather data based on the city name.
        /// </summary>
        /// <param name="cityName">The name of the city.</param>
        /// <returns>A <see cref="Task{WeatherData}"/> representing the asynchronous operation.
        /// The task result contains the <see cref="WeatherData"/> for the specified city.</returns>
        /// <exception cref="ArgumentException">Thrown if the city name is null or empty.</exception>
        /// <exception cref="Exception">Thrown if the city is not found.</exception>
        public async Task<WeatherData> GetWeatherByCityAsync(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ArgumentException("City name cannot be null or empty.");
            }

            // Call the geocoding API to get the latitude and longitude of the city
            var geocodingResponse = await _httpClient.GetStringAsync(
                $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(cityName)}&count=1&language=en&format=json");

            var geocodingJson = JObject.Parse(geocodingResponse);
            var results = geocodingJson["results"]?.FirstOrDefault();

            if (results == null)
            {
                throw new Exception("City not found.");
            }

            double latitude = (double)results["latitude"];
            double longitude = (double)results["longitude"];

            // Fetch weather data using latitude and longitude
            return await GetWeatherByLocationAsync(latitude, longitude);
        }
    }
}
