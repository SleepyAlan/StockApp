using Entities;
using ServiceContracts.DTOs;

namespace ServiceContracts.Extensions;

public static class SellOrderRequestExtensions
{
    public static SellOrder ToSellOrder(this SellOrderRequest? sellOrderRequest)
    {
        if (sellOrderRequest is null)
        {
            throw new ArgumentNullException(nameof(sellOrderRequest), "Sell order request cannot be null.");
        }
        return new SellOrder
        {
            StockSymbol = sellOrderRequest.StockSymbol,
            StockName = sellOrderRequest.StockName,
            DateAndTimeOfOrder = sellOrderRequest.DateAndTimeOfOrder,
            Quantity = sellOrderRequest.Quantity,
            Price = sellOrderRequest.Price
        };
    }
}
