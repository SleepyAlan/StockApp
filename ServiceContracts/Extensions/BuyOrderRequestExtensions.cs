using Entities;
using ServiceContracts.DTOs;

namespace ServiceContracts.Extensions;

public static class BuyOrderRequestExtensions
{
    public static BuyOrder ToBuyOrder(this BuyOrderRequest? buyOrderRequest)
    {
        if (buyOrderRequest is null)
        {
            throw new ArgumentNullException(nameof(buyOrderRequest), "Buy order request cannot be null.");
        }
        return new BuyOrder
        {
            StockSymbol = buyOrderRequest.StockSymbol,
            StockName = buyOrderRequest.StockName,
            DateAndTimeOfOrder = buyOrderRequest.DateAndTimeOfOrder,
            Quantity = buyOrderRequest.Quantity,
            Price = buyOrderRequest.Price
        };
    }
}
