using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public string Method()
    {
        return "Hello World";
    }
}