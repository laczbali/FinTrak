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

		public bool IsDbConnectionOK()
		{
			var connection = this._db.Database.GetDbConnection();
			if (connection.State == System.Data.ConnectionState.Closed)
			{
				connection.Open();
			}
			var command = connection.CreateCommand();
			command.CommandText = "select 1 from dual";
			var result = (int?)command.ExecuteScalar();

			return (result == 1);
		}
	}
}
