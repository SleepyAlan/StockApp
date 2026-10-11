using FinnhubServices.Models.BusinessModels;

namespace StockApp.ViewModels;

public class StockBusinessWrapper
{
    public CurrentStockQuote? Quote { get; set; }
    public StockInfo? Info { get; set; }
    public CompanyProfile? Profile { get; set; }
}
