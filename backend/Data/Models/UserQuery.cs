using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fintrak.Data.Models
{
	[Table("user_queries")]
	public class UserQuery
	{
		[Key]
		public string? Name { get; set; }

		public string? QueryJson { get; set; }
	}
}
