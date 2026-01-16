using System.ComponentModel.DataAnnotations;
using e_Commerce_Orders_App.CustomValidators;

namespace e_Commerce_Orders_App.Models;

public class Order
{
    public int? OrderNumber { get; set; }
    
    [Required(ErrorMessage = "{0} can't be blank")]
    [MinimumYearValidator(2000)]
    [Display(Name = "Order Date")]
    public DateTime? OrderDate { get; set; }
    
    [Required(ErrorMessage = "{0} can't be blank")]
    [Display(Name = "Invoice Price")]
    public double? InvoicePrice { get; set; }
    
    [Required(ErrorMessage = "{0} can't be blank")]
    [MinLength(1, ErrorMessage = "At least one product is required")]
    public List<Product> Products { get; set; }

    private double? CalculateSum()
    {
        return Products.Sum(p => p.Price * p.Quantity);
    }

    public bool IsInvoiceValid()
    {
        return InvoicePrice == CalculateSum();
    }
}