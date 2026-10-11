using FinnhubServices.Models.BusinessModels;

namespace StockApp.Models;

public class StockViewModel
{
    public CurrentStockQuote? Quote { get; set; }
    public StockInfo? Info { get; set; }
    public CompanyProfile? Profile { get; set; }
}
