using System.ComponentModel.DataAnnotations;

namespace e_Commerce_Orders_App.Models;

public class Product
{
    [Required(ErrorMessage = "{0} can't be blank")]
    [Display(Name = "Product Code")]
    public int? ProductCode { get; set; }
    
    [Required(ErrorMessage = "{0} can't be blank")]
    public double? Price { get; set; }
    
    [Required(ErrorMessage = "{0} can't be blank")]
    public int? Quantity { get; set; }
}