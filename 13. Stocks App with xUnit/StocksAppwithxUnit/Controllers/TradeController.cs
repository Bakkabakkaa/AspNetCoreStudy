using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StocksAppwithxUnit.Models;

namespace StocksAppwithxUnit.Controllers;

[Route("[controller]")]
public class TradeController : Controller
{
    private readonly TradingOptions _tradingOptions;
    private readonly IStocksService _stocksService;
    private readonly IFinnhubService _finnhubService;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Constructor for TradeController that executes when a new object is created for the class
    /// </summary>
    /// <param name="tradingOptions">Injecting TradeOptions config through Options pattern</param>
    /// <param name="stocksService">Injecting StocksService</param>
    /// <param name="finnhubService">Injecting FinnhubService</param>
    /// <param name="configuration">Injecting IConfiguration</param>
    public TradeController(IOptions<TradingOptions> options, IStocksService stocksService, IFinnhubService finnhubService, IConfiguration configuration)
    {
        _tradingOptions = options.Value;
        _stocksService = stocksService;
        _finnhubService = finnhubService;
        _configuration = configuration;
    }

    [Route("/")]
    public async Task<ViewResult> Index()
    {
        // Reset stock symbol if not exists
        if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
            _tradingOptions.DefaultStockSymbol = "MSFT";
        
        // Get company profile from API server
        Dictionary<string, object>? companyProfileDictionary = await 
            _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);
        
        // Get stock price quotes from API server
        Dictionary<string, object>? stockQuoteDictionary = await 
            _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);
        
        // Create model object
        StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };
        
        // Load data from finnHubService into model object
        if (companyProfileDictionary != null && stockQuoteDictionary != null)
        {
            stockTrade = new StockTrade()
            {
                StockSymbol = Convert.ToString(companyProfileDictionary["ticker"]),
                StockName = Convert.ToString(companyProfileDictionary["name"]),
                Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString())
            };
        }
        
        // Send Finnhub token to view
        ViewBag.FinnhubToken = _configuration["FinnhubToken"];

        return View(stockTrade);
    }
}