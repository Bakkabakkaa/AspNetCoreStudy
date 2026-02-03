using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services;

public class StocksService : IStocksService
{
    private readonly List<BuyOrder> _buyOrders;
    private readonly List<SellOrder> _sellOrders;

    public StocksService()
    {
        _sellOrders = new List<SellOrder>();
        _buyOrders = new List<BuyOrder>();
    }
    
    private IStocksService _stocksServiceImplementation;
    public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
    {
        // Validation: buyOrderRequest can't be null
        if (buyOrderRequest == null)
        {
            throw new ArgumentNullException(nameof(buyOrderRequest));
        }
        
        // Model validation
        ValidationHelper.ModelValidation(buyOrderRequest);

        // Convert buyOrderRequest int BuyOrder type
        BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
        
        // Generate BuyOrderID
        buyOrder.BuyOrderID = Guid.NewGuid();
        
        // Add buy order object to buy orders list
        _buyOrders.Add(buyOrder);
        
        // Convert the BuyOrder object int BuyOrderResponse type
        return buyOrder.ToBuyOrderResponse();
    }

    public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
    {
        // Validation: sellOrderRequest can't be null
        if (sellOrderRequest == null)
        {
            throw new ArgumentNullException(nameof(sellOrderRequest));
        }
        
        // Model validation
        ValidationHelper.ModelValidation(sellOrderRequest);
        
        // Convert sellOrderRequest int SellOrder type
        SellOrder sellOrder = sellOrderRequest.ToSellOrder();

        // Generate SellOrderID
        sellOrder.SellOrderID = Guid.NewGuid();
        
        // Add sell order object to sell orders list
        _sellOrders.Add(sellOrder);

        // Convert the SellOrder object into SellOrderResponse type
        return sellOrder.ToSellOrderResponse();
    }

    public List<BuyOrderResponse> GetBuyOrders()
    {
        return _buyOrders
            .OrderByDescending(temp => temp.DateAndTimeOfOrder)
            .Select(temp => temp.ToBuyOrderResponse()).ToList();

    }

    public List<SellOrderResponse> GetSellOrders()
    {
        return _stocksServiceImplementation.GetSellOrders();
    }
}