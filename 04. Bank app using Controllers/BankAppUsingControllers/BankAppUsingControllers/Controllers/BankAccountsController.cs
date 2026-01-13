using Microsoft.AspNetCore.Mvc;

namespace BankAppUsingControllers.Controllers;

public class BankAccountsController : Controller
{
    private int _accountNumber = 1001;
    private string _accountHolderName = "Example Name";
    private int _currentBalance = 5000;

    [HttpGet]
    [Route("/")]
    public IActionResult WelcomeMessage()
    {
        return Content("Welcome to the Best Bank");
    }

    [HttpGet]
    [Route("/account-details")]
    public IActionResult AccountDetails()
    {
        return Json(new
        {
            _accountNumber,
            _accountHolderName,
            _currentBalance
        });
    }

    [HttpGet]
    [Route("/account-statement")]
    public IActionResult AccountStatement()
    {
        return File("~/statement.pdf", "application/pdf");
    }

    [HttpGet]
    [Route("/get-current-balance/{accountNumber:int?}")]
    public IActionResult CurrentBalance([FromRoute] int? accountNumber)
    {
        if (accountNumber == _accountNumber)
        {
            return Content(_currentBalance.ToString());
        }
        else if (accountNumber == null)
        {
            HttpContext.Response.StatusCode = 404;
            return Content("Account Number should be supplied");
        }
        else
        {
            return BadRequest("Account Number should be 1001");
        }
    }
}