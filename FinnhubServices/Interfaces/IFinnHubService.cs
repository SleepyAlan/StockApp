using FinnhubServices.Models.BusinessModels;

namespace FinnhubServices.Interfaces;

public interface IFinnhubService
{
    Task<CurrentStockQuote> GetStockPriceQuote(string symbol);
    Task<CompanyProfile> GetCompanyProfile(string symbol);
}