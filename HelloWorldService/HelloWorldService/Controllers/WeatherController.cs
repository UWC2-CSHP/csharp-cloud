using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HelloWorldService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private const string appId = "e110d5fafce836782e6365db900193f8";
        private HttpClient httpClient;

        public WeatherController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/");
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            var url = string.Format("weather?appId={0}&q={1}&units=imperial", appId, city);

            var result = await httpClient.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();

                var weatherModel = JsonSerializer.Deserialize<Models.Root>(json);

                return Ok(weatherModel);
            }

            return BadRequest();
        }
    }
}
