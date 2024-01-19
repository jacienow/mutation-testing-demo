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
            // Arrange
            var serviceMock = new Mock<IWeather>();
            var loggerMock = new Mock<ILogger>();
            serviceMock.Setup(x => x.DownloadForecast()).Returns(Enumerable.Empty<WeatherForecast>());
            var controller = new WeatherForecastController(serviceMock.Object, loggerMock.Object);

            // Act
            var response = controller.GetWeatherForCity();

            // Assert
            Assert.IsType<NotFoundObjectResult>(response);
        }

        [Fact]
        public void GetWeatherForCity_WhenHasForecast_ShouldReturnOk()
        {
            // Arrange
            var serviceMock = new Mock<IWeather>();
            var loggerMock = new Mock<ILogger>();

            var sampleForecast = new WeatherForecast { City = "Szczecin", Date = DateTime.UtcNow };
            var forecastsToReturn = new List<WeatherForecast> { sampleForecast };

            serviceMock.Setup(x => x.DownloadForecast()).Returns(forecastsToReturn);
            var controller = new WeatherForecastController(serviceMock.Object, loggerMock.Object);

            // Act
            var response = controller.GetWeatherForCity();

            // Assert
            Assert.IsType<OkObjectResult>(response);
        }
    }
}