using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockApp.Options;
using StockApp.ViewModels;
using FinnhubServices.Models.BusinessModels;
using FinnhubServices.Interfaces;
using System.Text.Json;
using ServiceContracts;

namespace StockApp.Controllers;

public class TradeController : Controller
{
    private readonly string _defaultStockSymbol;
    private readonly IFinnhubService _finnhubService;
    private readonly IStocksService _stockService;

    public TradeController(
        IFinnhubService finnhubService,
        IStocksService stocksService,
        IOptions<TradingOptions> tradeOptions
        )
    {
        _defaultStockSymbol = tradeOptions.Value.DefaultStockSymbol;
        _finnhubService = finnhubService;
        _stockService = stocksService;
    }

    [Route("trade/{symbol?}")]
    public async Task<IActionResult> Index(string? symbol)
    {
        string stockSymbol = (symbol ?? _defaultStockSymbol).ToUpper();
        CurrentStockQuote quote;
        CompanyProfile profile;
        try
        {
            quote = await _finnhubService.GetStockPriceQuote(stockSymbol);
            profile = await _finnhubService.GetCompanyProfile(stockSymbol);
        }
        catch (Exception ex) when
            (ex is HttpRequestException || ex is InvalidOperationException)
        {
            ViewBag.Error = "There has been an error in trying to fetch data from Finnhub Api";
            return View("Error");
        }
        catch (Exception ex) when
            (ex is JsonException || ex is NotSupportedException ||
             ex is ArgumentNullException || ex is OperationCanceledException)
        {
            ViewBag.Error = "Failed to parse data from Finnhub Api. Contact support.";
            return View("Error");
        }

        var viewModel = new StockTrade()
        {
            StockName = profile.Name,
            StockSymbol = profile.TickerSymbol,
            Price = quote.CurrentPriceToday ?? default,
            Quantity = 0
        };
        return View(viewModel);
    }
}
