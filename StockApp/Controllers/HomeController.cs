using FinnhubServices.Interfaces;
using FinnhubServices.Models.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text.Json;

namespace StockApp.Controllers;

public class HomeController(IFinnhubService service) : Controller
{
    [Route("/")]
    public async Task<IActionResult> Index()
    {
        string symbol = "AAPL";
        CurrentStockQuote quoteDto;
        CompanyProfile profileDto;
        try
        {
            quoteDto = await service.GetStockPriceQuote(symbol);
            profileDto = await service.GetCompanyProfile(symbol);
        }
        catch (Exception ex) when
            (ex is HttpRequestException || ex is InvalidOperationException)
        {
            //ViewBag.Error = "There has been an error in trying to fetch data from Finnhub Api";
            //return View();
        }
        catch (Exception ex) when
            (ex is JsonException || ex is NotSupportedException ||
             ex is ArgumentNullException || ex is OperationCanceledException)
        {
            //ViewBag.Error = "Failed to parse data from Finnhub Api. Contact support.";
            //return View();
        }

        return Ok();
        // TODO: Convert to ViewModel... Inject into View
        //return View();
    }
}
