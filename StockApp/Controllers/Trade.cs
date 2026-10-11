using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockApp.Options;
using StockApp.ViewModels;
using FinnhubServices.Models.BusinessModels;
using FinnhubServices.Interfaces;
using System.Text.Json;
using FinnhubServices.Options;

namespace StockApp.Controllers;

public class Trade : Controller
{
    private readonly string _defaultStockSymbol;
    private readonly string _finnhubApiKey;
    private readonly IFinnhubService _service;

    public Trade(
        IFinnhubService finnhubService,
        IOptions<TradingOptions> tradeOptions,
        IOptions<ApiOptions> apiOptions
        )
    {
        _defaultStockSymbol = tradeOptions.Value.DefaultStockSymbol;
        _finnhubApiKey = apiOptions.Value.ApiKey;
        _service = finnhubService;
    }

    [Route("trade/{symbol?}")]
    public async Task<IActionResult> Index(string? symbol)
    {
        string stockSymbol = (symbol ?? _defaultStockSymbol).ToUpper();
        CurrentStockQuote quote;
        CompanyProfile profile;
        try
        {
            quote = await _service.GetStockPriceQuote(stockSymbol);
            profile = await _service.GetCompanyProfile(stockSymbol);
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
        ViewBag.ApiKey = _finnhubApiKey;
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
