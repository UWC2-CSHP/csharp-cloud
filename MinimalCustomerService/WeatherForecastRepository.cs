using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json.Serialization;

namespace MinimalCustomerService
{
    public interface IWeatherForecaseRepository
    {
        WeatherForecast[] GetForecasts();
    }

    public record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    //public struct WeatherForecast2
    //{
    //    [JsonPropertyName("date")]
    //    public DateTime Date { get; set; }
    //    public int TemperatureC { get; set; }
    //    public string? Summary { get; set; }
    //    public int TemperatureF
    //    {
    //        get
    //        {
    //            return 32 + (int)(TemperatureC / 0.5556);
    //        }
    //    }
    //}

    public class WeatherForecastRepository : IWeatherForecaseRepository
    {
        string[] summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild",
                "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

        public WeatherForecast[] GetForecasts()
        {
            var forecasts = Enumerable.Range(1, 5)
                    .Select(index =>
                      new WeatherForecast
                      (
                          DateTime.Now.AddDays(index),
                          Random.Shared.Next(-20, 55),
                          summaries[Random.Shared.Next(summaries.Length)]
                      ))
       .ToArray();

            return forecasts;
        }

    }
}
