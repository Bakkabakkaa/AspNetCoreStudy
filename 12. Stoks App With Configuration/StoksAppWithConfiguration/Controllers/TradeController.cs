using Microsoft.AspNetCore.Mvc;

namespace StoksAppWithConfiguration.Controllers;

public class TradeController : Controller
{
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }
}