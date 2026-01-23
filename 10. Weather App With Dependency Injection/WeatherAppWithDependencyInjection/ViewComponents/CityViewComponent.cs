using Microsoft.AspNetCore.Mvc;
using Models;

namespace WeatherAppWithDependencyInjection.ViewComponents;

public class CityViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(CityWeather city)
    {
        ViewBag.CityCssClass = GetCssClassByTemperature(city.TemperatureFahrenheit);

        return View(city);
    }

    private string GetCssClassByTemperature(int temperature)
    {
        switch (temperature)
        {
            case < 44:
                return "blue-back";
            case < 75:
                return "green-back";
            default:
                return "orange-back";
        }
    }
}