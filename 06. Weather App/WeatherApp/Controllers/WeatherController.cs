using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers;

public class WeatherController : Controller
{
    private List<CityWeather> cities = new List<CityWeather>()
    {
        new CityWeather(){ CityUniqueCode = "LDN", CityName = "London", DateAndTime = new DateTime(2030, 1, 1, 8, 0, 0), TemperatureFahrenheit = 33},
        new CityWeather(){ CityUniqueCode = "NYC", CityName = "London", DateAndTime = new DateTime(2030, 1, 1, 3, 0, 0), TemperatureFahrenheit = 60},
        new CityWeather(){CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = new DateTime(2030, 1, 1, 9, 0, 0), TemperatureFahrenheit = 82}
    };
    
    
}