using FinnhubServices.Models.DTOs;

namespace FinnhubServices.Interfaces;

// TODO: RETURN BUSINESS MODELS INSTEAD
internal interface IFinnHubService
{
    Task<StockQuoteDto> GetStockPriceQuote(string symbol);
    Task<CompanyProfileDto> GetCompanyProfile(string symbol);
}