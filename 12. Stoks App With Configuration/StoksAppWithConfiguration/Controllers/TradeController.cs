using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;

namespace StoksAppWithConfiguration.Controllers;

public class TradeController : Controller
{
    private readonly IFinnhubService _finnhubService;
    private readonly IOptions<TradingOptions> _tradingOptions;
    public TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions)
    {
        _finnhubService = finnhubService;
        _tradingOptions = tradingOptions;
    }
    [Route("/")]
    public async Task<IActionResult> Index()
    {
        Dictionary<string, object>? responseDictionary = await _finnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);
        return View();
    }
}