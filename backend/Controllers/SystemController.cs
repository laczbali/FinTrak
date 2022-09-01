using fintrak.Data;
using fintrak.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        var connection = this._db.Database.GetDbConnection();
        if(connection.State == System.Data.ConnectionState.Closed)
		{
            connection.Open();
		}
        var command = connection.CreateCommand();
        command.CommandText = "select 1 from dual";
        var result = command.ExecuteScalar();

        return Ok(result?.ToString());
    }

}
