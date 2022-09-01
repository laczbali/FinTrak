using System.ComponentModel.DataAnnotations.Schema;

namespace fintrak.Data.Models
{
	 [Table("transactions")]
	public class Transaction
	{
		public int Id { get; set; }

		public float Amount { get; set; }

		public DateTime Timestamp { get; set; }

		public string? Description { get; set; }

		public TransactionCategory? Category { get; set; }
	}
}
