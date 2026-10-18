using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTOs;

public class SellOrderResponse : IEquatable<SellOrderResponse?>
{
    public Guid SellOrderID { get; set; }
    public string? StockSymbol { get; set; }
    public string? StockName { get; set; }
    public DateTime DateAndTimeOfOrder { get; set; }
    public uint Quantity { get; set; }
    public double Price { get; set; }
    public double TradeAmount { get; set; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as SellOrderResponse);
    }

    public bool Equals(SellOrderResponse? other)
    {
        return other is not null &&
               SellOrderID.Equals(other.SellOrderID) &&
               StockSymbol == other.StockSymbol &&
               StockName == other.StockName &&
               DateAndTimeOfOrder == other.DateAndTimeOfOrder &&
               Quantity == other.Quantity &&
               Price == other.Price &&
               TradeAmount == other.TradeAmount;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(SellOrderID, StockSymbol, StockName, DateAndTimeOfOrder, Quantity, Price, TradeAmount);
    }
}
