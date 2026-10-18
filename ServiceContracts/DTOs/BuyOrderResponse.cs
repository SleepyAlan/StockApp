using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTOs;

public class BuyOrderResponse : IEquatable<BuyOrderResponse?>
{
    public Guid BuyOrderID { get; set; }
    public string? StockSymbol { get; set; }
    public string? StockName { get; set; }
    public DateTime DateAndTimeOfOrder { get; set; }
    public uint Quantity { get; set; }
    public double Price { get; set; }
    public double TradeAmount { get; set; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as BuyOrderResponse);
    }

    public bool Equals(BuyOrderResponse? other)
    {
        return other is not null &&
               BuyOrderID.Equals(other.BuyOrderID) &&
               StockSymbol == other.StockSymbol &&
               StockName == other.StockName &&
               DateAndTimeOfOrder == other.DateAndTimeOfOrder &&
               Quantity == other.Quantity &&
               Price == other.Price &&
               TradeAmount == other.TradeAmount;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(BuyOrderID, StockSymbol, StockName, DateAndTimeOfOrder, Quantity, Price, TradeAmount);
    }
}
