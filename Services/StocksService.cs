using Entities;
using ServiceContracts;
using ServiceContracts.DTOs;
using ServiceContracts.Extensions;
using Services.Helpers;


namespace Services;

public class StocksService : IStocksService
{
    // These will be used to store the buy and sell orders in memory
    // TODO: Swap these with a database or persistent storage in the future
    private readonly List<BuyOrderResponse> _buyOrders = new();
    private readonly List<SellOrderResponse> _sellOrders = new();


    public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
    {
        ArgumentNullException.ThrowIfNull(buyOrderRequest, nameof(buyOrderRequest));
        ModelValidationHelper.Validate(buyOrderRequest);

        BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

        //TODO: Swap this with a database in the future
        buyOrder.BuyOrderID = Guid.NewGuid();
        _buyOrders.Add(buyOrder.ToBuyOrderResponse());
        return Task.FromResult(buyOrder.ToBuyOrderResponse());
    }

    public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
    {
        ArgumentNullException.ThrowIfNull(sellOrderRequest, nameof(sellOrderRequest));
        ModelValidationHelper.Validate(sellOrderRequest);

        SellOrder sellOrder = sellOrderRequest.ToSellOrder();

        //TODO: Swap this with a database in the future
        sellOrder.SellOrderID = Guid.NewGuid();
        _sellOrders.Add(sellOrder.ToSellOrderResponse());
        return Task.FromResult(sellOrder.ToSellOrderResponse());
    }

    public Task<List<BuyOrderResponse>> GetAllBuyOrders()
    {
        //TODO: Swap this with a database in the future
        return Task.FromResult(_buyOrders);
    }

    public Task<List<SellOrderResponse>> GetAllSellOrders()
    {
        //TODO: Swap this with a database in the future
        return Task.FromResult(_sellOrders);
    }
}
