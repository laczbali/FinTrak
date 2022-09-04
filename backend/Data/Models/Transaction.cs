using fintrak.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace fintrak.Data.Models
{
	public class Transaction
	{
		[JsonPropertyName("pk")]
		public string Pk => $"TRANSACTION@{Id}";

		[JsonPropertyName("sk")]
		public string Sk => Pk;

		[JsonPropertyName("id")]
		public string Id { get; set; } = string.Empty;

		[Required]
		[JsonPropertyName("amount")]
		public float? Amount { get; set; }

		[Required]
		[JsonPropertyName("description")]
		public string? Description { get; set; }

		[JsonPropertyName("creationTime")]
		public DateTime? CreationTime { get; set; }

		[JsonPropertyName("category")]
		public string? Category { get; set; }
	}
}
