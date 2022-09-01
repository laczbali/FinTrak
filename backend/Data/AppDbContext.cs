﻿using fintrak.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace fintrak.Data
{
	public class AppDbContext : DbContext
	{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		public virtual DbSet<Transaction> TransactionCategories { get; set; }
		public virtual DbSet<Transaction> Transactions { get; set; }
	}
}