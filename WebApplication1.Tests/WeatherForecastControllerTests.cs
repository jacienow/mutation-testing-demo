using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApplication1.Controllers;
using WebApplication1.Interfaces;

namespace WebApplication1.Tests
{
    public class WeatherForecastControllerTests
    {
        [Fact]
        public void GetWeatherForCity_WhenNoForecast_ShouldReturnNotFound()
        {
            var serviceMock = new Mock<IWeather>();
            var loggerMock = new Mock<ILogger>();
            serviceMock.Setup(x => x.DownloadForecast()).Returns(Enumerable.Empty<WeatherForecast>());
            var controller = new WeatherForecastController(serviceMock.Object, loggerMock.Object);

            var response = controller.GetWeatherForCity();

            Assert.IsType<NotFoundObjectResult>(response);
        }

        [Fact]
        public void GetWeatherForCity_WhenHasForecast_ShouldReturnOk()
        {
            var forecasts = new List<WeatherForecast>();
            var forecast = new WeatherForecast { City = "Szczecin", Date = DateTime.UtcNow };
            forecasts.Add(forecast);
            var serviceMock = new Mock<IWeather>();
            var loggerMock = new Mock<ILogger>();
            serviceMock.Setup(x => x.DownloadForecast()).Returns(forecasts);
            var controller = new WeatherForecastController(serviceMock.Object, loggerMock.Object);

            var response = controller.GetWeatherForCity();

            Assert.IsType<OkObjectResult>(response);
        }
    }
}