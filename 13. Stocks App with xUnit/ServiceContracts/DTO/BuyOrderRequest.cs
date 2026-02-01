using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO;

/// <summary>
/// DTO class that represents a buy order to purchase the
/// stocks - that can be used while inserting / updating
/// </summary>
public class BuyOrderRequest
{
    [Required(ErrorMessage = "Stock Symbol can't be null or empty")]
    public string StockSymbol { get; set; }
    
    [Required(ErrorMessage = "Stock Name can't be null or empty")]
    public string StockName { get; set; }
    
    public DateTime DateAndTimeOfOrder { get; set; }
    
    [Range(1, 100000, ErrorMessage = "You can buy maximum of 100000 shares in single order. Minimum is 1.")]
    public uint Quantity { get; set; }
    
    [Range(1, 10000, ErrorMessage = "The maximum price of stock is 10000. Minimum is 1.")]
    public double Price { get; set; }

    /// <summary>
    /// Converts the current object of BuyOrderRequest into a new object of BuyOrder type
    /// </summary>
    /// <returns>A new object of BuyOrder class</returns>
    public BuyOrder ToBuyOrder()
    {
        return new BuyOrder()
        {
            StockSymbol = this.StockSymbol, StockName = this.StockName,
            DateAndTimeOfOrder = this.DateAndTimeOfOrder, Quantity = this.Quantity,
            Price = this.Price
        };
    }
}