using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StoksAppWithConfiguration.Models;

namespace StoksAppWithConfiguration.Controllers;

public class TradeController : Controller
{
    private readonly IFinnhubService _finnhubService;
    private readonly IOptions<TradingOptions> _tradingOptions;
    private readonly IConfiguration _configuration;
    public TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions,
        IConfiguration configuration)
    {
        _finnhubService = finnhubService;
        _tradingOptions = tradingOptions;
        _configuration = configuration;
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
            var priceElement = (JsonElement)dictionaryStockPriceQuote["c"];
            var symbolElement = (JsonElement)dictionaryCompanyProfile["ticker"];
            var nameElement = (JsonElement)dictionaryCompanyProfile["name"];

            stockTrade = new StockTrade
            {
                StockSymbol = symbolElement.GetString(),
                StockName = nameElement.GetString(),
                Price = priceElement.GetDouble()
            };
        }

        
        ViewBag.FinnhubToken = _configuration["FinnhubToken"];

        
        return View(stockTrade);
    }
}