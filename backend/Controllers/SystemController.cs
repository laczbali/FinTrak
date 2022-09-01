using fintrak.Data;
using fintrak.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace fintrak.Controllers;

[ApiController]
[Route("")]
public class SystemController : ControllerBase
{
	private readonly AppDbContext _db;

	public SystemController(AppDbContext db)
    {
		this._db = db;
	}

    [HttpGet("")]
    public IActionResult Hello()
    {
        return Ok("Hello World!");
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        // TODO return straight away if not in dev mode

        this._db.Transactions.Add(new Transaction { Amount = 100, Timestamp = DateTime.Now});
        this._db.SaveChanges();

        return Ok("ok");
    }

}
