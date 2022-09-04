using fintrak.Data.Models;
using fintrak.Data.Providers;
using Microsoft.AspNetCore.Mvc;

namespace fintrak.Controllers
{
	[ApiController]
	[Route("transactions")]
	public class TransactionController : ControllerBase
	{
		private readonly TransactionDbProvider _dbProvider;

		public TransactionController(TransactionDbProvider db)
		{
			this._dbProvider = db;
		}

		[HttpPost("")]
		public async Task<IActionResult> PostTransaction([FromBody] Transaction model)
		{
			var result = await this._dbProvider.SaveNewTransaction(model);
			return Ok(result);
		}

		[HttpPatch("")]
		public async Task<IActionResult> ChangeTransaction([FromBody] Transaction model)
		{
			return Ok(await this._dbProvider.ChangeTransaction(model));
		}

		[HttpDelete("")]
		public async Task<IActionResult> DeleteTransaction([FromBody] Transaction model)
		{
			return Ok(await this._dbProvider.DeleteTransaction(model));
		}
	}
}
