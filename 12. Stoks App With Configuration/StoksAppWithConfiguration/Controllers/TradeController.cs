using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StoksAppWithConfiguration.Models;

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
        if (_tradingOptions.Value.DefaultStockSymbol == null)
        {
            _tradingOptions.Value.DefaultStockSymbol = "MSFT";
        }
        
        Dictionary<string, object>? dictionaryStockPriceQuote = await _finnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);
        Dictionary<string, object>? dictionaryCompanyProfile = await _finnhubService.GetCompanyProfile(_tradingOptions.Value.DefaultStockSymbol);


        StockTrade stockTrade = new StockTrade();
        if (dictionaryCompanyProfile != null && dictionaryStockPriceQuote != null)
        {
            stockTrade = new StockTrade()
            {
                StockSymbol = Convert.ToString(dictionaryCompanyProfile["ticker"]),
                StockName = Convert.ToString(dictionaryCompanyProfile["name"]),
                Price = Convert.ToDouble(dictionaryStockPriceQuote["c"]),
            };
        }
        
        return View(stockTrade);
    }
}