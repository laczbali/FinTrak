using fintrak.Data.Models;
using fintrak.Models;

namespace fintrak.Data.Providers
{
	public class ReportDbProvider
	{

		public ReportDbProvider()
		{
		}

		public List<TransactionQueryResult> RunFilters(List<TransactionFilter> filters)
		{
			throw new NotImplementedException();
		}

		public UserQuery SaveUserQuery(UserQuery model)
		{
			throw new NotImplementedException();
		}

		public UserQuery? GetUserQuery(string name)
		{
			throw new NotImplementedException();
		}
	}
}
