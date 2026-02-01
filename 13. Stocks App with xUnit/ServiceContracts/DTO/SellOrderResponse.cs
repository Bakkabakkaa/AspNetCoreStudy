using Entities;

namespace ServiceContracts.DTO;

/// <summary>
/// DTO class that represents a sell order -
/// that can be used as return type of Stocks service
/// </summary>
public class SellOrderResponse
{
    public Guid SellOrderID { get; set; }
    public string StockSymbol { get; set; }
    public string StockName { get; set; }
    public DateTime DateAndTimeOfOrder { get; set; }
    public uint Quantity { get; set; }
    public double Price { get; set; }
    public double TradeAmount { get; set; }
}

public static class SellOrderExtensions
{
    /// <summary>
    /// An extension method to convert an object of SellOrder class into SellOrderResponse class
    /// </summary>
    /// <param name="sellOrder">The SellOrder object to convert</param>
    /// <returns>Returns the converted SellOrderResponse object</returns>
    public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
    {
        return new SellOrderResponse()
        {
            SellOrderID = sellOrder.SellOrderID, StockSymbol = sellOrder.StockSymbol,
            StockName = sellOrder.StockName, DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
            Quantity = sellOrder.Quantity, Price = sellOrder.Price,
            TradeAmount = sellOrder.Quantity * sellOrder.Price
        };
    }
}