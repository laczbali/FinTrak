using fintrak.Data.Providers;
using fintrak.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace fintrak.Controllers;

[ApiController]
[Route("")]
public class SystemController : ControllerBase
{

    public readonly SystemDbProvider _dbProvider;
    private readonly EnvHelper _env;

    public SystemController(SystemDbProvider dbProvider, EnvHelper env)
    {
        this._dbProvider = dbProvider;
        this._env = env;
    }

    [HttpGet("")]
    public IActionResult Hello()
    {
        return Ok("Hello World!");
    }

    [HttpPost("auth/login")]
    public async Task<IActionResult> Login([FromBody] string password)
    {
        if (password != this._env.Config.AuthToken)
            return Unauthorized();

        (var sessionToken, var sessionExpiry) = await this._dbProvider.NewSession();
        Response.Cookies.Append("Auth", sessionToken, new CookieOptions { Expires = sessionExpiry, SameSite = SameSiteMode.None, Secure = true });
        return Ok();
    }

    [HttpGet("auth/logout")]
    public IActionResult Logout()
    {
        this._dbProvider.ClearSessions();
        Response.Cookies.Delete("Auth");
        return Ok();
    }

    [HttpGet("auth/renew")]
    public async Task<IActionResult> Renew()
    {
        var sessionId = Request.Cookies.FirstOrDefault(x => x.Key == "Auth").Value;
        if(sessionId == null)
        {
            Response.Cookies.Delete("Auth");
            return BadRequest("Invalid cookie");
        }

        (var sessionToken, var sessionExpiry) = await this._dbProvider.RenewSession(sessionId);
        Response.Cookies.Append("Auth", sessionToken, new CookieOptions { Expires = sessionExpiry, SameSite = SameSiteMode.None, Secure = true });
        return Ok();
    }
}
