using fintrak.Data.Models;

namespace fintrak.Models
{
	public class TransactionQueryResult
	{
		public string FilterName { get; set; } = "";
		public List<Transaction> Transactions { get; set; } = new List<Transaction>();
	}
}
