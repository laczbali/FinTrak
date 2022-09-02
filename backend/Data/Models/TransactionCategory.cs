﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fintrak.Data.Models
{
	[Table("transaction_categories")]
	public class TransactionCategory
	{
		[Required]
		[Key]
		public string? Name { get; set; }
	}
}
