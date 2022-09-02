using fintrak.Data.Models;
using fintrak.Data.Providers;
using Microsoft.AspNetCore.Mvc;

namespace fintrak.Controllers
{
	[ApiController]
	[Route("transactions")]
	public class TransactionController : ControllerBase
	{
		private readonly BaseDbProvider _dbProvider;

		public TransactionController(BaseDbProvider db)
		{
			this._dbProvider = db;
		}

		[HttpPost("")]
		public IActionResult PostTransaction([FromBody] Transaction model)
		{
			return Ok(this._dbProvider.SaveNewTransaction(model));
		}

		[HttpPatch("")]
		public IActionResult ChangeTransaction([FromBody] Transaction model)
		{
			return Ok(this._dbProvider.ChangeTransaction(model));
		}
	}
}
