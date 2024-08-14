namespace FGMWeatherServiceAPI.Configurations
{
    /// <summary>
    /// Configuration settings for MongoDB connection, including the connection string,
    /// database name, and collection name.
    /// </summary>
    public class MongoDbSettings
    {
        /// <summary>
        /// Gets or sets the MongoDB connection string used to connect to the database.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the name of the MongoDB database.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the name of the MongoDB collection where data will be stored.
        /// </summary>
        public string CollectionName { get; set; }
    }
}
