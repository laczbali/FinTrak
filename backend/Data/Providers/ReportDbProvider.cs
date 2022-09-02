using fintrak.Data.Models;
using fintrak.Models;
using Microsoft.EntityFrameworkCore;

namespace fintrak.Data.Providers
{
	public class ReportDbProvider
	{
		private readonly AppDbContext _db;

		public ReportDbProvider(AppDbContext db)
		{
			_db = db;
		}

		public List<TransactionQueryResult> RunFilters(List<TransactionFilter> filters)
		{
			var results = new List<TransactionQueryResult>();
			foreach (var filter in filters)
			{
				var queryResult = new TransactionQueryResult() { FilterName = filter.FilterName};
				queryResult.Transactions = this._db.Transactions
					.Include("Category")
					.Where(t => 
					(filter.CategoryIn == null || filter.CategoryIn.Contains(t.CategoryName))
					&& (filter.AmountBelow == null || t.Amount < filter.AmountBelow)
					&& (filter.AmountAbove == null || t.Amount > filter.AmountAbove)
					&& (filter.CreatedBefore == null || t.Timestamp < filter.CreatedBefore)
					&& (filter.CreatedAfter == null || t.Timestamp > filter.CreatedAfter)
				).ToList();

				results.Add(queryResult);
			}

			return results;
		}

		public UserQuery SaveUserQuery(UserQuery model)
		{
			this._db.UserQueries.Add(model);
			this._db.SaveChanges();
			return model;
		}

		public UserQuery? GetUserQuery(string name)
		{
			return this._db.UserQueries.FirstOrDefault(q => q.Name == name);
		}
	}
}
