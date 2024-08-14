using Moq;
using MongoDB.Driver;
using Xunit;
using FGMWeatherServiceAPI.Services;
using FGMWeatherServiceAPI.Models;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FGMWeatherServiceAPI.Configurations;

public class WeatherServiceTests
{
    /// <summary>
    /// Tests that GetWeatherByLocationAsync returns the correct WeatherData when it is found in the database.
    /// </summary>
    [Fact]
    public async Task GetWeatherByLocationAsync_ReturnsWeatherData_WhenDataIsFound()
    {
        // Arrange
        var mockHttpClient = new Mock<HttpClient>();
        var mockMongoClient = new Mock<IMongoClient>();
        var mockDatabase = new Mock<IMongoDatabase>();
        var mockWeatherCollection = new Mock<IMongoCollection<WeatherData>>();

        var weatherData = new WeatherData { Latitude = 40.7128, Longitude = -74.0060, Temperature = 22.0 };

        // Mocking the cursor
        var mockCursor = new Mock<IAsyncCursor<WeatherData>>();
        mockCursor.SetupSequence(cursor => cursor.MoveNext(It.IsAny<CancellationToken>()))
                  .Returns(true)
                  .Returns(false);
        mockCursor.Setup(cursor => cursor.Current)
                  .Returns(new List<WeatherData> { weatherData });

        // Mocking the FindFluent and cursor
        var mockFindFluent = new Mock<IFindFluent<WeatherData, WeatherData>>();
        mockFindFluent.Setup(fl => fl.ToCursorAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(mockCursor.Object);

        // Mock the collection's Find method
        mockWeatherCollection.Setup(collection => collection.Find(It.IsAny<FilterDefinition<WeatherData>>(), null))
                             .Returns(mockFindFluent.Object);

        // Set up the database mock to return the collection
        mockDatabase.Setup(db => db.GetCollection<WeatherData>(It.IsAny<string>(), null))
                    .Returns(mockWeatherCollection.Object);

        // Set up the client mock to return the database
        mockMongoClient.Setup(client => client.GetDatabase(It.IsAny<string>(), null))
                       .Returns(mockDatabase.Object);

        // Create a MongoDbSettings instance for the WeatherService
        var mongoDbSettings = new MongoDbSettings
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "WeatherDb",
            CollectionName = "WeatherData"
        };

        // Create an instance of WeatherService with mocked dependencies
        var weatherService = new WeatherService(mockHttpClient.Object, mockMongoClient.Object, Options.Create(mongoDbSettings));

        // Act
        // Call the method under test
        var result = await weatherService.GetWeatherByLocationAsync(40.7128, -74.0060);

        // Assert
        // Verify that the result is not null and contains the expected temperature
        Assert.NotNull(result);
        Assert.Equal(22.0, result.Temperature);
    }
}
