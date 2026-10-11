using FinnhubServices.Models.BusinessModels;

namespace FinnhubServices.Interfaces;

public interface IFinnhubService
{
    string GetApiKey();
    Task<CurrentStockQuote> GetStockPriceQuote(string symbol);
    Task<CompanyProfile> GetCompanyProfile(string symbol);
}