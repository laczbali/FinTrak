using Microsoft.AspNetCore.Mvc;

namespace fintrak.Controllers;

[ApiController]
[Route("")]
public class SystemController : ControllerBase
{
    public SystemController()
    {
    }

    [HttpGet("")]
    public IActionResult Hello()
    {
        return Ok("Hello World!");
    }

}
