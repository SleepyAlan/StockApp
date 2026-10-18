using Newtonsoft.Json.Linq;
using ServiceContracts;
using ServiceContracts.DTOs;
using System.Collections;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.Tests;

public class StocksServiceTests
{
    private readonly IStocksService _stockService;
    public StocksServiceTests()
    {
        _stockService = new StocksService();
    }

    #region CreateBuyOrder Tests
    [Fact]
    public async Task CreateBuyOrder_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        BuyOrderRequest? buyOrderRequest = null;
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _stockService.CreateBuyOrder(buyOrderRequest));
    }

    [Fact]
    public async Task CreateBuyOrder_QuantityIsZero_ThrowsArgumentException()
    {
        // Arrange
        var buyOrderRequest = new BuyOrderRequest
        {
            StockSymbol = "AAPL",
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 0, // Invalid quantity (minimum is 1, maximum is 100000)
            Price = 150.0
        };
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _stockService.CreateBuyOrder(buyOrderRequest));
    }

    [Fact]
    public async Task CreateBuyOrder_QuantityIsTooHigh_ThrowsArgumentException()
    {
        // Arrange
        var buyOrderRequest = new BuyOrderRequest
        {
            StockSymbol = "AAPL",
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 100001, // Invalid quantity (minimum is 1, maximum is 100000)
            Price = 150.0
        };
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _stockService.CreateBuyOrder(buyOrderRequest));
    }

    [Fact]
    public async Task CreateBuyOrder_PriceIsZero_ThrowsArgumentException()
    {
        // Arrange
        var buyOrderRequest = new BuyOrderRequest
        {
            StockSymbol = "AAPL",
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 10,
            Price = 0 // Invalid price (minimum is 1, maximum is 10000)
        };
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _stockService.CreateBuyOrder(buyOrderRequest));
    }

    [Fact]
    public async Task CreateBuyOrder_PriceIsTooHigh_ThrowsArgumentException()
    {
        // Arrange
        var buyOrderRequest = new BuyOrderRequest
        {
            StockSymbol = "AAPL",
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 10,
            Price = 10001 // Invalid price (minimum is 1, maximum is 10000)
        };
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _stockService.CreateBuyOrder(buyOrderRequest));
    }

    [Fact]
    public async Task CreateBuyOrder_StockSymbolIsNull_ThrowsArgumentException()
    {
        // Arrange
        var buyOrderRequest = new BuyOrderRequest
        {
            StockSymbol = null, // Invalid stock symbol (can't be null)
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 10,
            Price = 150.0
        };
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _stockService.CreateBuyOrder(buyOrderRequest));
    }

    [Fact]
    public async Task CreateBuyOrder_DateAndTimeOfOrderIsBefore2000_ThrowsArgumentException()
    {
        // Arrange
        var buyOrderRequest = new BuyOrderRequest
        {
            StockSymbol = "AAPL",
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = new DateTime(1999, 12, 31), // Invalid date (should be equal or newer than 2000-01-01)
            Quantity = 10,
            Price = 150.0
        };
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _stockService.CreateBuyOrder(buyOrderRequest));
    }

    [Fact]
    public async Task CreateBuyOrder_ValidRequest_ReturnsBuyOrderResponse()
    {
        // Arrange
        var buyOrderRequest = new BuyOrderRequest
        {
            StockSymbol = "AAPL",
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 10,
            Price = 150.0
        };
        // Act
        BuyOrderResponse result = await _stockService.CreateBuyOrder(buyOrderRequest);
        // Assert
        Assert.NotNull(result);
        Assert.IsType<BuyOrderResponse>(result);
        Assert.NotEqual(Guid.Empty, result.BuyOrderID);
        Assert.Equal(buyOrderRequest.StockSymbol, result.StockSymbol);
        Assert.Equal(buyOrderRequest.StockName, result.StockName);
        Assert.Equal(buyOrderRequest.DateAndTimeOfOrder, result.DateAndTimeOfOrder);
        Assert.Equal(buyOrderRequest.Quantity, result.Quantity);
        Assert.Equal(buyOrderRequest.Price, result.Price);
        Assert.True(result.TradeAmount == buyOrderRequest.Quantity * buyOrderRequest.Price);
    }
    #endregion

    #region CreateSellOrder Tests
    [Fact]
    public async Task CreateSellOrder_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        SellOrderRequest? sellOrderRequest = null;
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _stockService.CreateSellOrder(sellOrderRequest));
    }

    [Fact]
    public async Task CreateSellOrder_QuantityIsZero_ThrowsArgumentException()
    {
        // Arrange
        var sellOrderRequest = new SellOrderRequest
        {
            StockSymbol = "AAPL",
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 0, // Invalid quantity (minimum is 1, maximum is 100000)
            Price = 150.0
        };
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _stockService.CreateSellOrder(sellOrderRequest));
    }

    [Fact]
    public async Task CreateSellOrder_QuantityIsTooHigh_ThrowsArgumentException()
    {        
        // Arrange
        var sellOrderRequest = new SellOrderRequest
        {
            StockSymbol = "AAPL",
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 100001, // Invalid quantity (minimum is 1, maximum is 100000)
            Price = 150.0
        };
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _stockService.CreateSellOrder(sellOrderRequest));
    }

    [Fact]
    public async Task CreateSellOrder_PriceIsZero_ThrowsArgumentException()
    {
        // Arrange
        var sellOrderRequest = new SellOrderRequest
        {
            StockSymbol = "AAPL",
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 10,
            Price = 0 // Invalid price (minimum is 1, maximum is 10000)
        };
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _stockService.CreateSellOrder(sellOrderRequest));
    }

    [Fact]
    public async Task CreateSellOrder_PriceIsTooHigh_ThrowsArgumentException()
    {
        // Arrange
        var sellOrderRequest = new SellOrderRequest
        {
            StockSymbol = "AAPL",
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 10,
            Price = 10001 // Invalid price (minimum is 1, maximum is 10000)
        };
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _stockService.CreateSellOrder(sellOrderRequest));
    }

    [Fact]
    public async Task CreateSellOrder_StockSymbolIsNull_ThrowsArgumentException()
    {
        // Arrange
        var sellOrderRequest = new SellOrderRequest
        {
            StockSymbol = null, // Invalid stock symbol (can't be null)
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 10,
            Price = 150.0
        };
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _stockService.CreateSellOrder(sellOrderRequest));
    }

    [Fact]
    public async Task CreateSellOrder_DateAndTimeOfOrderIsBefore2000_ThrowsArgumentException()
    {
        // Arrange
        var sellOrderRequest = new SellOrderRequest
        {
            StockSymbol = "AAPL",
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = new DateTime(1999, 12, 31), // Invalid date (should be equal or newer than 2000-01-01)
            Quantity = 10,
            Price = 150.0
        };
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _stockService.CreateSellOrder(sellOrderRequest));
    }

    [Fact]
    public async Task CreateSellOrder_AllValidValues_ReturnsSellOrderResponse()
    {
        // Arrange
        var sellOrderRequest = new SellOrderRequest
        {
            StockSymbol = "AAPL",
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 10,
            Price = 150.0
        };
        // Act
        SellOrderResponse result = await _stockService.CreateSellOrder(sellOrderRequest);
        // Assert
        Assert.NotNull(result);
        Assert.IsType<SellOrderResponse>(result);
        Assert.NotEqual(Guid.Empty, result.SellOrderID);
        Assert.Equal(sellOrderRequest.StockSymbol, result.StockSymbol);
        Assert.Equal(sellOrderRequest.StockName, result.StockName);
        Assert.Equal(sellOrderRequest.DateAndTimeOfOrder, result.DateAndTimeOfOrder);
        Assert.Equal(sellOrderRequest.Quantity, result.Quantity);
        Assert.Equal(sellOrderRequest.Price, result.Price);
        Assert.True(result.TradeAmount == sellOrderRequest.Quantity * sellOrderRequest.Price);
    }

    #endregion

    #region GetAllBuyOrders Tests
    [Fact]
    public async Task GetAllBuyOrders_Default_ReturnsEmptyList()
    {
        // Act
        var result = await _stockService.GetAllBuyOrders();
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllBuyOrders_AfterAddingBuyOrders_ReturnsAllBuyOrders()
    {
        // Arrange
        var buyOrderRequest1 = new BuyOrderRequest
        {
            StockSymbol = "AAPL",
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 10,
            Price = 150.0
        };
        var buyOrderRequest2 = new BuyOrderRequest
        {
            StockSymbol = "GOOGL",
            StockName = "Alphabet Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 5,
            Price = 2800.0
        };
        BuyOrderResponse? responseExpected1 = await _stockService.CreateBuyOrder(buyOrderRequest1);
        BuyOrderResponse? responseExpected2 = await _stockService.CreateBuyOrder(buyOrderRequest2);

        // Act
        var result = await _stockService.GetAllBuyOrders();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        BuyOrderResponse? responseActual1 = result.FirstOrDefault(order => order.StockSymbol == buyOrderRequest1.StockSymbol);
        Assert.NotNull(responseActual1);
        Assert.Equal(responseExpected1, responseActual1);

        BuyOrderResponse? responseActual2 = result.FirstOrDefault(order => order.StockSymbol == buyOrderRequest2.StockSymbol);
        Assert.NotNull(responseActual2);
        Assert.Equal(responseExpected2, responseActual2);
    }
    #endregion


    #region GetAllSellOrders Tests
    [Fact]
    public async Task GetAllSellOrders_Default_ReturnsEmptyList()
    {
        // Act
        var result = await _stockService.GetAllSellOrders();
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllSellOrders_AfterAddingSellOrders_ReturnsAllSellOrders()
    {
        // Arrange
        var sellOrderRequest1 = new SellOrderRequest
        {
            StockSymbol = "AAPL",
            StockName = "Apple Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 10,
            Price = 150.0
        };
        var sellOrderRequest2 = new SellOrderRequest
        {
            StockSymbol = "GOOGL",
            StockName = "Alphabet Inc.",
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = 5,
            Price = 2800.0
        };
        SellOrderResponse? responseExpected1 = await _stockService.CreateSellOrder(sellOrderRequest1);
        SellOrderResponse? responseExpected2 = await _stockService.CreateSellOrder(sellOrderRequest2);

        // Act
        var result = await _stockService.GetAllSellOrders();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        SellOrderResponse? responseActual1 = result.FirstOrDefault(order => order.StockSymbol == sellOrderRequest1.StockSymbol);
        Assert.NotNull(responseActual1);
        Assert.Equal(responseExpected1, responseActual1);

        SellOrderResponse? responseActual2 = result.FirstOrDefault(order => order.StockSymbol == sellOrderRequest2.StockSymbol);
        Assert.NotNull(responseActual2);
        Assert.Equal(responseExpected2, responseActual2);
    }
    #endregion

    #region Helpers

    #endregion
}