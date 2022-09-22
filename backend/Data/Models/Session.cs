using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace fintrak.Data.Models
{
	public class Session
	{
		[JsonPropertyName("pk")]
		public string Pk => $"SESSION@{Guid}";

		[JsonPropertyName("sk")]
		public string Sk => Pk;

		[JsonPropertyName("guid")]
		public string Guid { get; set; } = string.Empty;

		[JsonPropertyName("expires")]
		public DateTime Expires { get; set; }
	}
}
