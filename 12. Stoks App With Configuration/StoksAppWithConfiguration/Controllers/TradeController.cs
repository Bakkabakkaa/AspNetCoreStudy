using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace StoksAppWithConfiguration.Controllers;

public class TradeController : Controller
{
    private readonly IFinnhubService _finnhubService;
    public TradeController(IFinnhubService finnhubService)
    {
        _finnhubService = finnhubService;
    }
    [Route("/")]
    public async Task<IActionResult> Index()
    {
        Dictionary<string, object>? responseDictionary = await _finnhubService.GetStockPriceQuote("MSFT");
        return View();
    }
}