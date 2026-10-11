namespace FinnhubServices.Models.BusinessModels;

public class CurrentStockQuote
{
    public decimal? CurrentPriceToday { get; set; }
    public decimal? Change { get; set; }
    public decimal? PercentChange { get; set; }
    public decimal? HighPriceOfTheDay { get; set; }
    public decimal? LowPriceOfTheDay { get; set; }
    public decimal? OpenPrice { get; set; }
    public decimal? PreviousClose { get; set; }
}
