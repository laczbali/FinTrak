using System.ComponentModel.DataAnnotations.Schema;

namespace fintrak.Data.Models
{
	[Table("transaction_categories")]
	public class TransactionCategory
	{
		public int Id { get; set; }

		public string? Name { get; set; }
	}
}
