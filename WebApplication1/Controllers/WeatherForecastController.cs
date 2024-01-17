using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IWeather _weatherService;

        public WeatherForecastController(IWeather weatherService, ILogger logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }


        [HttpGet(Name = "GetWeatherForCity")]
        public IActionResult GetWeatherForCity()
        {
            var forecast = _weatherService.DownloadForecast();

            if (!forecast.Any()) 
            {
                return new NotFoundObjectResult(forecast);
            }

            var forecastsForCity = forecast.Where(x => x.City == "Szczecin");

            var filteredForecasts = new List<WeatherForecast>();

            foreach (var forecastForCity in forecastsForCity)
            {
                if (forecastForCity.Date >= DateTime.UtcNow.AddDays(-5))
                {
                    filteredForecasts.Add(forecastForCity);
                }
            }

            return new OkObjectResult(filteredForecasts);
        }
    }
}
