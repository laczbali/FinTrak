namespace fintrak.Data.Models
{
	public class Transaction
	{
		public int Id { get; set; }

		public float Amount { get; set; }

		public DateTime Timestamp { get; set; }

		public string? Description { get; set; }

		public TransactionCategory? Category { get; set; }
	}
}
