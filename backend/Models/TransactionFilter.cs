using System.ComponentModel.DataAnnotations;

namespace fintrak.Models
{
	public class TransactionFilter
	{
		[Required]
		public string FilterName { get; set; } = "";
		public int? AmountBelow { get; set; }
		public int? AmountAbove { get; set; }
		public DateTime? CreatedBefore { get; set; }
		public DateTime? CreatedAfter { get; set; }
		public List<string?>? CategoryIn { get; set; }
	}
}
