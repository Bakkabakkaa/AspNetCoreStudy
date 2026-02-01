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