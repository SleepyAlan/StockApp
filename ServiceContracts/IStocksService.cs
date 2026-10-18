using ServiceContracts.DTOs;

namespace ServiceContracts;

public interface IStocksService
{
    /// <summary>
    /// Creates a buy order asynchronously.
    /// </summary>
    /// <remarks>Performs asynchronous I/O and may throw exceptions for validation or transport
    /// errors.</remarks>
    /// <param name="buyOrderRequest">Buy order DTO which is details to submit; may be null.</param>
    /// <returns>A task thats results in a BuyOrderResponse DTO reprsenting the BuyOrder.</returns>
    Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

    /// <summary>
    /// Creates a sell order asynchronously.
    /// </summary>
    /// <param name="sellOrderRequest">Sell order DTO which is details to submit; may be null.</param>
    /// <returns>A task thats results in a SellOrderResponse DTO reprsenting the SellOrder.</returns>
    Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

    /// <summary>
    /// Asynchronously retrieves all buy orders.
    /// </summary>
    /// <returns>A task that results in a list of BuyOrderResponse DTO objects.</returns>
    Task<List<BuyOrderResponse>> GetAllBuyOrders();

    /// <summary>
    /// Asynchronously retrieves the current sell orders.
    /// </summary>
    /// <remarks>The returned list may be empty if no sell orders exist. Exceptions can be thrown for network,
    /// validation, or authorization failures.</remarks>
    /// <returns>A Task that results in a list of SellOrderResponse DTO objects.</returns>
    Task<List<SellOrderResponse>> GetAllSellOrders();
}
