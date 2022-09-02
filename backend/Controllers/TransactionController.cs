using fintrak.Data;
using fintrak.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace fintrak.Controllers
{
	[ApiController]
	[Route("transactions")]
	public class TransactionController : ControllerBase
	{
		private readonly DbProvider _dbProvider;

		public TransactionController(DbProvider db)
		{
			this._dbProvider = db;
		}

		[HttpPost("")]
		public IActionResult PostTransaction([FromBody] Transaction model)
		{
			if(model is null || !this.ModelState.IsValid) return BadRequest(this.ModelState);

			var result = this._dbProvider.SaveNewTransaction(model);
			return Ok(result);
		}
	}
}
