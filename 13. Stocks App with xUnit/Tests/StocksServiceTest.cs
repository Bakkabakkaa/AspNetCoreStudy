using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace Tests;

public class StocksServiceTest
{
    private readonly IStocksService _stocksService;

    public StocksServiceTest()
    {
        _stocksService = new StocksService();
    }

    #region CreateBuyOrder

    [Fact]
    public void CreateBuyOrder_NullBuyOrder_ToBeArgumentNullException()
    {
        // Arrange
        BuyOrderRequest? buyOrderRequest = null;
        
        // Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            // Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Theory]
    [InlineData(0)]
    public void CreateBuyOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint buyOrderQuantity)
    {
        // Arrange
        BuyOrderRequest? buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.Quantity = buyOrderQuantity;
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }
    
    [Theory]
    [InlineData(100001)]
    public void CreateBuyOrder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint buyOrderQuantity)
    {
        // Arrange
        BuyOrderRequest? buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.Quantity = buyOrderQuantity;
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
             // Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Theory]
    [InlineData(0)]
    public void CreateBuyOrder_PriceIsLessThanMinimum_ToBeArgumentException(uint buyOrderPrice)
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.Price = buyOrderPrice;
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }
    
    [Theory]
    [InlineData(10001)]
    public void CreateBuyOrder_PriceIsGreaterThanMaximum_ToBeArgumentException(uint buyOrderPrice)
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.Price = buyOrderPrice;
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public void CreateBuyOrder_StockSymbolIsNull_ToBeArgumentException()
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.StockSymbol = null;
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public void CreateBuyOrder_DateOfOrderIsLessThanYear2000_ToBeArgumentException()
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31");
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _stocksService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public void CreateBuyOrder_ValidData_ToBeSuccessful()
    {
        // Arrange
        BuyOrderRequest? buyOrderRequest = CreateValidBuyOrderRequest();
        
        // Act
        BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(buyOrderRequest);
        
        // Assert
        Assert.NotEqual(Guid.Empty, buyOrderResponse.BuyOrderID);
    }

    #endregion

    private BuyOrderRequest CreateValidBuyOrderRequest()
    {
        return new BuyOrderRequest()
        {
            StockSymbol = "MSFT", StockName = "Microsoft",
            Price = 10, Quantity = 10,
            DateAndTimeOfOrder = new DateTime(2026, 2, 3)
        };
    }
}