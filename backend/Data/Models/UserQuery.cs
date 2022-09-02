using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fintrak.Data.Models
{
	[Table("user_queries")]
	public class UserQuery
	{
		[Required]
		[Key]
		public string? Name { get; set; }

		[Required]
		public string? QueryJson { get; set; }
	}
}
