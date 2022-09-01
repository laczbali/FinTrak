using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fintrak.Data.Models
{
	 [Table("transactions")]
	public class Transaction
	{
		public int Id { get; set; }

		[Required]
		public float? Amount { get; set; }

		public DateTime Timestamp { get; set; }

		[Required]
		public string? Description { get; set; }

		public TransactionCategory? Category { get; set; }
	}
}
