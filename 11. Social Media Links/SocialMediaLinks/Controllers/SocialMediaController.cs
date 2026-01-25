using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SocialMediaLinks.Controllers;

public class SocialMediaController : Controller
{
    private readonly SocialMediaLinksOptions _socialMediaLinksOptions;
    
    public SocialMediaController(IOptions<SocialMediaLinksOptions> socialMediaLinksOptions)
    {
        _socialMediaLinksOptions = socialMediaLinksOptions.Value;
    }
    
    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        ViewBag.SocialMedia = _socialMediaLinksOptions;
        return View();
    }
}