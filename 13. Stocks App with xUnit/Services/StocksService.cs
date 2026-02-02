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
        return _stocksServiceImplementation.CreateSellOrder(sellOrderRequest);
    }

    public List<BuyOrderResponse> GetBuyOrders()
    {
        return _stocksServiceImplementation.GetBuyOrders();
    }

    public List<SellOrderResponse> GetSellOrders()
    {
        return _stocksServiceImplementation.GetSellOrders();
    }
}