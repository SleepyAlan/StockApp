namespace FinnhubServices.Models.BusinessModels;

public class StockInfo
{
    public required string Symbol { get; set; }
    public required string DisplaySymbol { get; set; }
    public string? SymbolDescription { get; set; }
    public string? SecurityType { get; set; }
    public string? Currency { get; set; }
}
