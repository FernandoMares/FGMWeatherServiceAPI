# FGMWeatherServiceAPI
This project is a .NET 6 Web API that provides weather data based on location or city name. The weather data includes temperature, wind direction, wind speed, and sunrise time. The API interacts with the Open-Meteo API to retrieve real-time weather information and caches results in a MongoDB database.

# Note
 Please use ``master`` branch to run the application.

# Table of Contents
Project Structure

Project Structure

Prerequisites

Installation

Configuration

Usage

API Endpoints

Technologies Used


# Project Structure
The project is organized as follows:

Configurations

 - MongoDbSettings
   
Controllers

 - WeatherController

Models

 - Location
 - WeatherData

Serialization
 - CustomDateTimeSerializer

Services
 - IWeatherService
 - WeatherService

Program.cs

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [MongoDB](https://www.mongodb.com/try/download/community) installed locally or in the cloud
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or another C# compatible IDE

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/FernandoMares/FGMWeatherServiceAPI.git
   cd FGMWeatherServiceAPI

2. **Restore the .NET dependencies:**
   
  ```bash
    dotnet restore
  ```

3. **Configure your MongoDB connection in the appsettings.json file.**

4. **Build and run the application:**
  ```bash
    dotnet restore
  ```

## Configuration

The MongoDB settings are configured in the `appsettings.json` file:

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "WeatherDb",
    "CollectionName": "WeatherData"
  }
}
```
Ensure MongoDB is running and accessible at the connection string provided.

## Usage

Once the application is running, you can access the Swagger UI to explore the API:

**Swagger UI**: [http://localhost:<port>/swagger](http://localhost:<port>/swagger)

Replace `<port>` with the port number on which your application is running.

### API Endpoints

#### Get Weather by Location

- **Endpoint**: `/location`
- **Method**: `GET`
- **Query Parameters**:
  - `latitude` (double): Latitude of the location.
  - `longitude` (double): Longitude of the location.
- **Response**: Returns weather data for the specified coordinates.

#### Get Weather by City

- **Endpoint**: `/city`
- **Method**: `GET`
- **Query Parameters**:
  - `city` (string): Name of the city.
- **Response**: Returns weather data for the specified city.

## Technologies Used

- **.NET 6**: The framework used for building the application.
- **MongoDB**: The database used for storing weather data.
- **Open-Meteo API**: Provides weather data for the application.
- **Swagger**: Used for API documentation and exploration.
