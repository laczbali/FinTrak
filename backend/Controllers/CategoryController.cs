using fintrak.Data;
using fintrak.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace fintrak.Controllers
{
	[ApiController]
	[Route("categories")]
	public class CategoryController : ControllerBase
	{
		private readonly DbProvider _dbProvider;

		public CategoryController(DbProvider dbProvider)
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
			if(model is null || !this.ModelState.IsValid) return BadRequest(this.ModelState);

			var result = this._dbProvider.NewTransactionCategory(model);
			return Ok(result);
		}

		[HttpDelete("{name}")]
		public IActionResult Delete(string name)
		{
			if (string.IsNullOrEmpty(name)) return BadRequest("Must provide name");

			this._dbProvider.RemoveTransactionCategory(name);
			return Ok();
		}
	}
}
