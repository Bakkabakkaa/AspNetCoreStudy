using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace WeatherAppWithDependencyInjection.Controllers;

public class WeatherController : Controller
{
    private readonly IWeatherService _weatherService;
    
    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }
    
    [Route("/")]
    public IActionResult Index()
    {
        var cities = _weatherService.GetWeatherDetails();
        
        return View(cities);

    }

    [Route("weather/{cityCode?}")]
    public IActionResult City(string cityCode)
    {
        var city = _weatherService.GetWeatherByCityCode(cityCode);
        return View(city);
    }
}