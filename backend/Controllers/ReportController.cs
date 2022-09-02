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
		public IActionResult RunFilters([FromBody] List<TransactionFilter> filters)
		{
			return Ok(this._dbProvider.RunFilters(filters));
		}

		[HttpPost("user-query")]
		public IActionResult SaveUserQuery([FromBody] UserQuery model)
		{
			return Ok(this._dbProvider.SaveUserQuery(model));
		}

		[HttpGet("user-query/{name}")]
		public IActionResult GetUserQuery(string name)
		{
			return Ok(this._dbProvider.GetUserQuery(name));
		}
	}
}
