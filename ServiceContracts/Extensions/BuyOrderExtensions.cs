using Entities;
using ServiceContracts.DTOs;

namespace ServiceContracts.Extensions;

public static class BuyOrderExtensions
{
    public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
    {
        if (buyOrder is null)
        {
            throw new ArgumentNullException(nameof(buyOrder), "Buy order cannot be null.");
        }
        return new BuyOrderResponse
        {
            BuyOrderID = buyOrder.BuyOrderID,
            StockSymbol = buyOrder.StockSymbol,
            StockName = buyOrder.StockName,
            DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
            Quantity = buyOrder.Quantity,
            Price = buyOrder.Price,
            TradeAmount = buyOrder.Quantity * buyOrder.Price
        };
    }
}
