namespace FinnhubServices.Models.BusinessModels;

public class CompanyProfile
{
    //Company ticker symbol as used on the listed exchange.
    public string TickerSymbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string Exchange { get; set; } = string.Empty;
    public string? Industry { get; set; }
    public string? WebUrl { get; set; }
    public string? LogoUrl { get; set; }
    public DateTime? IpoDate { get; set; }
    public decimal MarketCapitalization { get; set; }
    public decimal SharesOutstanding { get; set; }
    public string? PhoneNumber { get; set; }
}