using fintrak.Data.Models;
using fintrak.Data.Providers;
using Microsoft.AspNetCore.Mvc;

namespace fintrak.Controllers
{
	[ApiController]
	[Route("categories")]
	public class CategoryController : ControllerBase
	{
		private readonly BaseDbProvider _dbProvider;

		public CategoryController(BaseDbProvider dbProvider)
		{
			this._dbProvider = dbProvider;
		}

		[HttpGet("")]
		public IActionResult GetAllCategories()
		{
			return Ok(_dbProvider.GetAllCategories());
		}

		[HttpPost("")]
		public IActionResult CreateNew([FromBody] TransactionCategory model)
		{
			return Ok(this._dbProvider.NewTransactionCategory(model));
		}

		[HttpDelete("{name}")]
		public IActionResult Delete(string name)
		{
			this._dbProvider.RemoveTransactionCategory(name);
			return Ok();
		}
	}
}
