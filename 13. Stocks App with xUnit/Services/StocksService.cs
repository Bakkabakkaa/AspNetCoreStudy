using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class StocksService : IStocksService
{
    private IStocksService _stocksServiceImplementation;
    public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
    {
        return _stocksServiceImplementation.CreateBuyOrder(buyOrderRequest);
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