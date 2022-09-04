using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using fintrak.Data.Models;
using fintrak.Helpers;
using System.Net;
using System.Text.Json;

namespace fintrak.Data.Providers
{
	public class TransactionDbProvider
	{
		private readonly IAmazonDynamoDB _dynamoDB;
		private readonly DbHelper _dbHelper;

		public TransactionDbProvider(IAmazonDynamoDB amazonDynamoDB, DbHelper dbHelper)
		{
			this._dynamoDB = amazonDynamoDB;
			this._dbHelper = dbHelper;
		}

		public async Task<List<string>> NewTransactionCategory(string name)
		{
			var categories = await GetAllCategories();
			if (categories.Contains(name))
				throw new ArgumentException($"Category [{name}] is already in DB");

			categories.Add(name);

			var model = new TransactionCategories
			{
				pk = "TRANSACTIONCATEGORIES",
				sk = "TRANSACTIONCATEGORIES",
				categories = categories
			};

			var createRequest = new PutItemRequest
			{
				TableName = this._dbHelper.DbTableName,
				Item = this._dbHelper.ModelToItem(model)
			};

			var response = await this._dynamoDB.PutItemAsync(createRequest);
			if (response.HttpStatusCode != HttpStatusCode.OK)
				throw new Exception("Failed to save category");
			return categories;
		}

		public async Task<List<string>> GetAllCategories()
		{
			var getRequest = new GetItemRequest
			{
				TableName = this._dbHelper.DbTableName,
				Key = new Dictionary<string, AttributeValue>
				{
					{"pk", new AttributeValue{ S = "TRANSACTIONCATEGORIES" } },
					{"sk", new AttributeValue{ S = "TRANSACTIONCATEGORIES" } }
				}
			};

			var response = await this._dynamoDB.GetItemAsync(getRequest);
			if (response.HttpStatusCode != HttpStatusCode.OK)
				throw new Exception("Failed to query categories");
			if (response.Item.Count == 0)
				return new List<string>();

			var categories = new List<string>() { "none" };
			var storedCategories = this._dbHelper.ItemToModel<TransactionCategories>(response.Item)?.categories ?? new List<string>();
			categories.AddRange(storedCategories);
			return categories;
		}

		public async Task<List<string>> RemoveTransactionCategory(string name)
		{
			var categories = await GetAllCategories();
			if (!categories.Contains(name))
				return categories;

			categories.Remove(name);

			var model = new TransactionCategories
			{
				pk = "TRANSACTIONCATEGORIES",
				sk = "TRANSACTIONCATEGORIES",
				categories = categories
			};

			var updateRequest = new PutItemRequest
			{
				TableName = this._dbHelper.DbTableName,
				Item = this._dbHelper.ModelToItem(model)
			};

			var response = await this._dynamoDB.PutItemAsync(updateRequest);
			if (response.HttpStatusCode != HttpStatusCode.OK)
				throw new Exception("Failed to delete category");
			return categories;
		}

		public async Task<Transaction> SaveNewTransaction(Transaction model, bool generateGuid = true)
		{
			if (model.Category != null)
			{
				var categories = await GetAllCategories();
				if (!categories.Contains(model.Category))
					throw new ArgumentException($"Invalid category [{model.Category}]");
			}
			else
			{
				model.Category = "none";
			}

			if (model.CreationTime == null) model.CreationTime = DateTime.Now;
			
			if(generateGuid)
				model.Id = Guid.NewGuid().ToString();

			var createRequest = new PutItemRequest
			{
				TableName = this._dbHelper.DbTableName,
				Item = this._dbHelper.ModelToItem(model)
			};

			var response = await this._dynamoDB.PutItemAsync(createRequest);
			if (response.HttpStatusCode != HttpStatusCode.OK)
				throw new Exception("Failed to save transaction");

			return model;
		}

		public async Task<Transaction> GetTransaction(Transaction model)
		{
			var getRequest = new GetItemRequest
			{
				TableName = this._dbHelper.DbTableName,
				Key = new Dictionary<string, AttributeValue>
				{
					{"pk", new AttributeValue{ S = model.Pk } },
					{"sk", new AttributeValue{ S = model.Sk } }
				}
			};

			var response = await this._dynamoDB.GetItemAsync(getRequest);
			if (response.HttpStatusCode != HttpStatusCode.OK)
				throw new Exception("Failed to query transaction");
			if (response.Item.Count == 0)
				throw new ArgumentException("Transaction not found");

			return this._dbHelper.ItemToModel<Transaction>(response.Item) ?? throw new Exception("Returned item is null");
		}

		public async Task<Transaction> ChangeTransaction(Transaction model)
		{
			// validate that the transaction already exists
			await GetTransaction(model);
			// save edits
			return await SaveNewTransaction(model, generateGuid: false);
		}

		public async Task<bool> DeleteTransaction(Transaction model)
		{
			var deleteRequest = new DeleteItemRequest
			{
				TableName = this._dbHelper.DbTableName,
				Key = new Dictionary<string, AttributeValue>
				{
					{"pk", new AttributeValue{ S = model.Pk } },
					{"sk", new AttributeValue{ S = model.Sk } }
				}
			};

			var response = await this._dynamoDB.DeleteItemAsync(deleteRequest);
			if (response.HttpStatusCode != HttpStatusCode.OK)
				throw new Exception("Failed to delete transaction");
			return true;
		}

		private class TransactionCategories
		{
			public string pk { get; set; } = string.Empty;
			public string sk { get; set; } = string.Empty;
			public List<string> categories { get; set; } = new List<string>();
		}
	}
}
