using fintrak.Data.Models;
using fintrak.Data.Providers;
using fintrak.Models;
using Microsoft.AspNetCore.Mvc;

namespace fintrak.Controllers
{
	[ApiController]
	[Route("reports")]
	public class ReportController : ControllerBase
	{
		private readonly ReportDbProvider _dbProvider;

		public ReportController(ReportDbProvider dbProvider)
		{
			this._dbProvider = dbProvider;
		}

		[HttpPost("run-filters")]
		public async Task<IActionResult> RunFilters([FromBody] List<TransactionFilter> filters)
		{
			return Ok(await this._dbProvider.RunFilters(filters));
		}

		[HttpPost("user-query")]
		public async Task<IActionResult> SaveUserQuery([FromBody] UserQuery model)
		{
			return Ok(await this._dbProvider.SaveUserQuery(model));
		}

		[HttpGet("user-query/{name}")]
		public async Task<IActionResult> GetUserQuery(string name)
		{
			return Ok(await this._dbProvider.GetUserQuery(name));
		}

		[HttpDelete("user-query/{name}")]
		public async Task<IActionResult> DeleteUserQuery(string name)
		{
			return Ok(await this._dbProvider.DeleteUserQuery(name));
		}
	}
}
