using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class TestController : Controller
    {
        [HttpGet("api/user")]
        public IActionResult Get()
        {
            return Ok(new { name = "Soufiane"});
        }
    }
}
