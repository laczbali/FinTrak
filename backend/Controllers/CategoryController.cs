using fintrak.Data.Models;
using fintrak.Data.Providers;
using Microsoft.AspNetCore.Mvc;

namespace fintrak.Controllers
{
	[ApiController]
	[Route("categories")]
	public class CategoryController : ControllerBase
	{
		private readonly TransactionDbProvider _dbProvider;

		public CategoryController(TransactionDbProvider dbProvider)
		{
			this._dbProvider = dbProvider;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAllCategories()
		{
			return Ok(await _dbProvider.GetAllCategories());
		}

		[HttpPost("{name}")]
		public async Task<IActionResult> CreateNew(string name)
		{
			return Ok(await this._dbProvider.NewTransactionCategory(name));
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			return Ok(await this._dbProvider.RemoveTransactionCategory(name));
		}
	}
}
