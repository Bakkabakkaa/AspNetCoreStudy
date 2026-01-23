namespace WeatherAppWithDependencyInjection.Models;

public class CityWeather
{
    public string CityUniqueCode { get; set; }
    public string CityName { get; set; }
    public string DateAndTime { get; set; }
    public string TemperatureFahrenheit { get; set; }
}