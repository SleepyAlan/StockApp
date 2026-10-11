namespace StockApp.ViewModels;

public class StockTrade
{
    public string? StockSymbol { get; set; }
    public string? StockName { get; set; }
    public decimal Price { get; set; }
    public ulong Quantity { get; set; }
}
