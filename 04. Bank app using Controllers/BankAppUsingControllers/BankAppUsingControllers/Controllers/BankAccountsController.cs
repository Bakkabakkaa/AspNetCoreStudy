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
}