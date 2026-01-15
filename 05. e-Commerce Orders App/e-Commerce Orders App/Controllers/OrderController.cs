using e_Commerce_Orders_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_Commerce_Orders_App.Controllers;

public class OrderController : Controller
{
    [HttpPost]
    [Route("/order")]
    public IActionResult CreateOrder([FromBody] Order order)
    {
        if (!ModelState.IsValid)
        {
            string errors = string.Join("\n",
                ModelState.Values.SelectMany(value => value.Errors
                    .Select(err => err.ErrorMessage)));

            return BadRequest(errors);
        }

        order.OrderNumber = Random.Shared.Next(1, 100000);

        return Json(new
        {
            order.OrderNumber
        });

    }
}