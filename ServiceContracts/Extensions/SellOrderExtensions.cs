using Entities;
using ServiceContracts.DTOs;

namespace ServiceContracts.Extensions;

public static class SellOrderExtensions
{
    public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
    {
        if (sellOrder is null)
        {
            throw new ArgumentNullException(nameof(sellOrder), "Sell order cannot be null.");
        }
        return new SellOrderResponse
        {
            SellOrderID = sellOrder.SellOrderID,
            StockSymbol = sellOrder.StockSymbol,
            StockName = sellOrder.StockName,
            DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
            Quantity = sellOrder.Quantity,
            Price = sellOrder.Price,
            TradeAmount = sellOrder.Quantity * sellOrder.Price
        };
    }
}
