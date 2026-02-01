using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO;

/// <summary>
/// DTO class that represents a buy order to purchase the
/// stocks - that can be used as return type of Stocks service
/// </summary>
public class BuyOrderResponse
{
    public Guid BuyOrderID { get; set; }
    public string StockSymbol { get; set; }
    public string StockName { get; set; }
    public DateTime DateAndTimeOfOrder { get; set; }
    public uint Quantity { get; set; }
    public double Price { get; set; }
    public double TradeAmount { get; set; }
}

public static class BuyOrderExtensions
{
    /// <summary>
    /// An extension method to convert an object of BuyOrder class into BuyOrderResponse class
    /// </summary>
    /// <param name="buyOrder">The BuyOrder object to convert</param>
    /// <returns>Returns the converted BuyOrderResponse object</returns>
    public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
    {
        return new BuyOrderResponse()
        {
            BuyOrderID = buyOrder.BuyOrderID, StockSymbol = buyOrder.StockSymbol,
            StockName = buyOrder.StockName, DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
            Quantity = buyOrder.Quantity, Price = buyOrder.Price,
            TradeAmount = buyOrder.Price * buyOrder.Quantity
        };
    }
}