namespace WebApplication1.Interfaces
{
    public interface IWeather
    {
        IEnumerable<WeatherForecast> DownloadForecast();
    }
}
