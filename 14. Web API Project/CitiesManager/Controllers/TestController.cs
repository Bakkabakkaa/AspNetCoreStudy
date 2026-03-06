using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.Controllers
{
    public class TestController : CustomControllerBase
    {
        [HttpGet]
        public string Method()
        {
            return "Hello World";
        }
    }
}