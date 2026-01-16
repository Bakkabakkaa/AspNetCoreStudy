using e_Commerce_Orders_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_Commerce_Orders_App.Controllers;

public class OrderController : Controller
{
    [HttpPost]
    [Route("/order")]
    public IActionResult CreateOrder([Bind("OrderDate,InvoicePrice,Products")][FromForm] Order order)
    {
        if (!ModelState.IsValid)
        {
            string errors = string.Join("\n",
                ModelState.Values.SelectMany(value => value.Errors
                    .Select(err => err.ErrorMessage)));

            return BadRequest(errors);
        }

        if (!order.IsInvoiceValid())
        {
            return BadRequest("Invoice Price doesn't match with the total cost of the specified products in the order.");
        }

        order.OrderNumber = Random.Shared.Next(1, 100000);

        return Json(new
        {
            order.OrderNumber
        });

    }
}