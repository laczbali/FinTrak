using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using fintrak.Data.Models;
using fintrak.Helpers;
using fintrak.Models;
using System.Net;
using System.Text.Json;

namespace fintrak.Data.Providers
{
	public class ReportDbProvider
	{

		private readonly IAmazonDynamoDB _dynamoDB;
		private readonly DbHelper _dbHelper;

		public ReportDbProvider(DbHelper dbHelper, IAmazonDynamoDB dynamoDB)
		{
			_dbHelper = dbHelper;
			_dynamoDB = dynamoDB;
		}

		public async Task<List<TransactionQueryResult>> RunFilters(List<TransactionFilter> filters)
		{
			var result = new List<TransactionQueryResult>();

			foreach (var filter in filters)
			{
				var filterExpression = "contains (pk, :id_prefix)";
				var filterValues = new Dictionary<string, AttributeValue> { { ":id_prefix", new AttributeValue { S = "TRANSACTION@" } } };

				string StripString(string input) => input.Substring(1, input.Length - 2);
				string DateToString(DateTime date) => StripString(JsonSerializer.Serialize(date));

				if (filter.AmountBelow != null)
				{
					filterExpression += " and amount < :amount_below";
					filterValues.Add(":amount_below", new AttributeValue { N = filter.AmountBelow.ToString() });
				}

				if (filter.AmountAbove != null)
				{
					filterExpression += " and amount > :amount_above";
					filterValues.Add(":amount_above", new AttributeValue { N = filter.AmountAbove.ToString() });
				}

				if (filter.CreatedBefore != null)
				{
					filterExpression += " and creationTime < :creationTime_before";
					filterValues.Add(":creationTime_before", new AttributeValue { S = DateToString(filter.CreatedBefore ?? DateTime.MaxValue) });
				}

				if (filter.CreatedAfter != null)
				{
					filterExpression += " and creationTime > :creationTime_after";
					filterValues.Add(":creationTime_after", new AttributeValue { S = DateToString(filter.CreatedAfter ?? DateTime.MinValue) });
				}

				if (filter.CategoryIn != null)
				{
					var categoryList = "";
					for (int i = 0; i < filter.CategoryIn.Count; i++)
					{
						var filterValue = $":c{i}";
						categoryList += $"{filterValue},";
						filterValues.Add(filterValue, new AttributeValue { S = filter.CategoryIn[i] });

					}
					categoryList = categoryList.Substring(0, categoryList.Length - 1);

					filterExpression += $" and category IN ({categoryList})";
				}

				var scanRequest = new ScanRequest
				{
					TableName = this._dbHelper.DbTableName,
					FilterExpression = filterExpression,
					ExpressionAttributeValues = filterValues
				};

				var response = await this._dynamoDB.ScanAsync(scanRequest);
				if (response.HttpStatusCode != HttpStatusCode.OK)
					throw new Exception("Failed to query transactions");

				var queryResult = new TransactionQueryResult();
				queryResult.FilterName = filter.FilterName;

				foreach (var item in response.Items)
				{
					var transaction = this._dbHelper.ItemToModel<Transaction>(item);

					if (transaction != null)
					{
						queryResult.Transactions.Add(transaction);
					}
				}

				result.Add(queryResult);
			}

			return result;
		}

		public async Task<UserQuery> SaveUserQuery(UserQuery model)
		{
			var createRequest = new PutItemRequest
			{
				TableName = this._dbHelper.DbTableName,
				Item = this._dbHelper.ModelToItem(model)
			};

			var response = await _dynamoDB.PutItemAsync(createRequest);
			if (response.HttpStatusCode != HttpStatusCode.OK)
				throw new Exception("Failed to save user query");
			return model;
		}

		public async Task<UserQuery?> GetUserQuery(string name)
		{
			var model = new UserQuery();
			model.Name = name;

			var getRequest = new GetItemRequest
			{
				TableName = this._dbHelper.DbTableName,
				Key = new Dictionary<string, AttributeValue>
				{
					{"pk", new AttributeValue{ S = model.Pk } },
					{"sk", new AttributeValue{ S = model.Sk } }
				}
			};

			var response = await _dynamoDB.GetItemAsync(getRequest);
			if (response.HttpStatusCode != HttpStatusCode.OK)
				throw new Exception("Failed to get user query");
			if (response.Item.Count == 0)
				return null;

			return this._dbHelper.ItemToModel<UserQuery>(response.Item);
		}

		public async Task<bool> DeleteUserQuery(string name)
		{
			var model = new UserQuery();
			model.Name = name;

			var deleteRequest = new DeleteItemRequest
			{
				TableName = this._dbHelper.DbTableName,
				Key = new Dictionary<string, AttributeValue>
				{
					{"pk", new AttributeValue{ S = model.Pk } },
					{"sk", new AttributeValue{ S = model.Sk } }
				}
			};

			var response = await _dynamoDB.DeleteItemAsync(deleteRequest);
			if (response.HttpStatusCode != HttpStatusCode.OK)
				throw new Exception("Failed to delete user query");
			return true;
		}
	}
}
