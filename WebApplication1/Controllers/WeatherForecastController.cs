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
            var weatherForecasts = _weatherService.DownloadForecast();

            if (!weatherForecasts.Any())
            {
                return new NotFoundObjectResult(weatherForecasts);
            }

            var forecastsForCity = weatherForecasts.Where(x => x.City == "Szczecin");

            var filteredForecasts = new List<WeatherForecast>();

            foreach (var forecast in forecastsForCity)
            {
                if (forecast.Date >= DateTime.UtcNow.AddDays(-5))
                {
                    filteredForecasts.Add(forecast);
                    _logger.LogInformation("forecast added!");
                }
            }

            return new OkObjectResult(filteredForecasts.OrderBy(x => x.Date).FirstOrDefault());
        }
    }
}
