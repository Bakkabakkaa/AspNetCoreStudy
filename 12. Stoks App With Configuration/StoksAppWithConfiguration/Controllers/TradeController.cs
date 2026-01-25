using Microsoft.AspNetCore.Mvc;

namespace StoksAppWithConfiguration.Controllers;

public class TradeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}