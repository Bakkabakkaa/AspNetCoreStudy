using Microsoft.AspNetCore.Mvc;

namespace SocialMediaLinks.Controllers;

public class SocialMediaController : Controller
{
    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        
        return View();
    }
}