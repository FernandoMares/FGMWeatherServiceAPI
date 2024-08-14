using FGMWeatherServiceAPI.Configurations;
using FGMWeatherServiceAPI.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure MongoDB settings using the "MongoDbSettings" section from the configuration.
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Register a singleton service for MongoClient using the connection string from the configuration.
builder.Services.AddSingleton<IMongoClient, MongoClient>(
    sp => new MongoClient(builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString")));

// Register the WeatherService as a scoped service, so a new instance is created per request.
builder.Services.AddScoped<IWeatherService, WeatherService>();

// Register an HTTP client for making API requests.
builder.Services.AddHttpClient();

// Register controllers to the service container, enabling the use of MVC patterns.
builder.Services.AddControllers();

// Add Swagger services to generate API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

// If the application is in the development environment, use Swagger and Swagger UI.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enforce HTTPS redirection to ensure secure communication.
app.UseHttpsRedirection();

// Use authorization middleware to protect endpoints.
app.UseAuthorization();

// Map controller endpoints to the request pipeline.
app.MapControllers();

// Run the application.
app.Run();
