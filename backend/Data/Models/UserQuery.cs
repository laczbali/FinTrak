using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace fintrak.Data.Models
{
	public class UserQuery
	{
		[JsonPropertyName("pk")]
		public string Pk => $"USERQUERY@{Name}";

		[JsonPropertyName("sk")]
		public string Sk => Pk;

		[Required]
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		[Required]
		[JsonPropertyName("queryJson")]
		public string QueryJson { get; set; } = string.Empty;
	}
}
