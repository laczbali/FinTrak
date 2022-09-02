using fintrak.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace fintrak.Data.Providers
{
	public class BaseDbProvider
	{
		private readonly AppDbContext _db;

		public BaseDbProvider(AppDbContext db)
		{
			_db = db;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<TransactionCategory> GetAllCategories()
		{
			return _db.TransactionCategories.ToList();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public TransactionCategory? NewTransactionCategory(TransactionCategory model)
		{
			_db.Add(model);
			_db.SaveChanges();
			return model;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="categoryName"></param>
		/// <exception cref="NotImplementedException"></exception>
		public void RemoveTransactionCategory(string categoryName)
		{
			var savedCategory = _db.TransactionCategories.FirstOrDefault(x => x.Name == categoryName);
			if (savedCategory == null) throw new ArgumentException($"Couldn't find category with name of [{categoryName}]");

			var transactionsOfCategory = _db.Transactions
				.Where(x => x.Category == savedCategory);
			foreach (var t in transactionsOfCategory)
			{
				t.Category = null;
				t.CategoryName = null;
			}

			_db.TransactionCategories.Remove(savedCategory);
			_db.SaveChanges();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public Transaction? SaveNewTransaction(Transaction model)
		{
			if (model.Timestamp == DateTime.MinValue) model.Timestamp = DateTime.UtcNow;
			if (model.Id != 0) model.Id = 0;

			if (model.Category != null)
			{
				var savedCategory = _db.TransactionCategories.FirstOrDefault(x => x.Name == model.Category.Name);
				model.Category = savedCategory;
			}

			_db.Transactions.Add(model);
			_db.SaveChanges();

			return model;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public Transaction? ChangeTransaction(Transaction model)
		{
			var savedTransaction = _db.Transactions.FirstOrDefault(x => x.Id == model.Id);
			if (savedTransaction == null) throw new ArgumentException($"Couldn't find transaction with ID of [{model.Id}]");

			if (model.Timestamp == DateTime.MinValue) model.Timestamp = DateTime.UtcNow;
			if (model.Category != null)
			{
				model.Category = _db.TransactionCategories.FirstOrDefault(x => x.Name == model.Category.Name);
			}

			savedTransaction.Description = model.Description;
			savedTransaction.Timestamp = model.Timestamp;
			savedTransaction.Amount = model.Amount;

			savedTransaction.Category = model.Category;
			if (model.Category == null) savedTransaction.CategoryName = null;

			_db.SaveChanges();
			return savedTransaction;
		}
	}
}
