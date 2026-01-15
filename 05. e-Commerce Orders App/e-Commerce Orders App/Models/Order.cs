namespace e_Commerce_Orders_App.Models;

public class Order
{
    public int? OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    private double InvoicePrice { get; set; }
    public List<Product> Products { get; set; }
}