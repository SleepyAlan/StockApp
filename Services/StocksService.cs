using ServiceContracts;
using ServiceContracts.DTOs;

namespace Services;

public class StocksService : IStocksService
{
    // These will be used to store the buy and sell orders in memory
    // TODO: Swap these with a database or persistent storage in the future
    private readonly List<BuyOrderResponse> _buyOrders = new();
    private readonly List<SellOrderResponse> _sellOrders = new();


    public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
    {
        throw new NotImplementedException();
    }

    public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
    {
        throw new NotImplementedException();
    }

    public Task<List<BuyOrderResponse>> GetAllBuyOrders()
    {
        throw new NotImplementedException();
    }

    public Task<List<SellOrderResponse>> GetAllSellOrders()
    {
        throw new NotImplementedException();
    }
}
