using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using FGMWeatherServiceAPI.Serialization;

namespace FGMWeatherServiceAPI.Models
{
    /// <summary>
    /// Represents weather data for a specific location.
    /// </summary>
    public class WeatherData
    {
        /// <summary>
        /// Id - Gets or sets the unique identifier for the weather data document in MongoDB.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Temperature - Gets or sets the temperature recorded at the location.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// WindDirection - Gets or sets the wind direction in degrees at the location.
        /// </summary>
        public int WindDirection { get; set; }

        /// <summary>
        /// WindSpeed - Gets or sets the wind speed measured at the location.
        /// </summary>
        public double WindSpeed { get; set; }

        /// <summary>
        /// Sunrise - Gets or sets the sunrise time at the location. Stored using a custom serializer for proper handling of time zones.
        /// </summary>

        [BsonSerializer(typeof(CustomDateTimeSerializer))]
        public DateTime Sunrise { get; set; }

        /// <summary>
        /// Latitude - Gets or sets the latitude of the location.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude - Gets or sets the longitude of the location.
        /// </summary>
        public double Longitude { get; set; }
    }
}
