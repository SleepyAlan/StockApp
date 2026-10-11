using FinnhubServices.Interfaces;
using FinnhubServices.Models.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using StockApp.ViewModels;
using System.Text.Json;

namespace StockApp.Controllers;

public class HomeController(IFinnhubService service) : Controller
{
    [Route("/")]
    public async Task<IActionResult> Index()
    {
        string symbol = "AAPL";
        CurrentStockQuote quote;
        CompanyProfile profile;
        try
        {
            quote = await service.GetStockPriceQuote(symbol);
            profile = await service.GetCompanyProfile(symbol);
        }
        catch (Exception ex) when
            (ex is HttpRequestException || ex is InvalidOperationException)
        {
            ViewBag.Error = "There has been an error in trying to fetch data from Finnhub Api";
            return View();
        }
        catch (Exception ex) when
            (ex is JsonException || ex is NotSupportedException ||
             ex is ArgumentNullException || ex is OperationCanceledException)
        {
            ViewBag.Error = "Failed to parse data from Finnhub Api. Contact support.";
            return View();
        }

        var viewModel = new StockBusinessWrapper()
        {
            Profile = profile,
            Quote = quote
        };

        return View(viewModel);
    }
}
