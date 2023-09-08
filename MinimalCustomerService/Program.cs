using MinimalCustomerService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IWeatherForecaseRepository, WeatherForecastRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI 
// at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/weatherforecast", (IWeatherForecaseRepository weatherForecaseRepository) =>
{
    var forecasts = weatherForecaseRepository.GetForecasts();

    return forecasts;
})
.WithName("GetWeatherForecast");

app.Run();

