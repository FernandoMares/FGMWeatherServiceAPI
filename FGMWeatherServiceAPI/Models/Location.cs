namespace FGMWeatherServiceAPI.Models
{
    /// <summary>
    /// Represents a geographic location with latitude, longitude, and city name.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Gets or sets the latitude of the location.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the location.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the city name of the location.
        /// </summary>
        public string City { get; set; }
    }
}
