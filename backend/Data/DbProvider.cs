using fintrak.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace fintrak.Data
{
	public class DbProvider
	{
		private readonly AppDbContext _db;

		public DbProvider(AppDbContext db)
		{
			this._db = db;
		}

		public object? IsDbConnectionOK()
		{
			var connection = this._db.Database.GetDbConnection();
			if (connection.State == System.Data.ConnectionState.Closed)
			{
				connection.Open();
			}
			var command = connection.CreateCommand();
			command.CommandText = "select 1 from dual";
			var result = (long)(command.ExecuteScalar() ?? 0);

			return (result == 1);
		}

		public Transaction SaveNewTransaction(Transaction model)
		{
			if (model.Timestamp == DateTime.MinValue) model.Timestamp = DateTime.UtcNow;
			if (model.Id != 0) model.Id = 0;

			if(model.Category != null)
			{
				
			}

			//this._db.Transactions.Add(model);
			//this._db.SaveChanges();

			return model;
		}
	}
}
