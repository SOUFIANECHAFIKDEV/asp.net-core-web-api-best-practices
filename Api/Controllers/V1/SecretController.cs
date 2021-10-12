using Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V1
{
    [ApiKeyAuth]
    public class SecretController : ControllerBase
    {
        [HttpPost("secret")]
        public IActionResult GetSecret()
        {
            return Ok("I have on secrets");
        }
    }
}
