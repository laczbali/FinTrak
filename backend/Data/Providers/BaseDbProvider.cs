using Amazon.DynamoDBv2;
using fintrak.Data.Models;

namespace fintrak.Data.Providers
{
	public class BaseDbProvider
	{
		private readonly IAmazonDynamoDB _amazonDynamoDB;

		public BaseDbProvider(IAmazonDynamoDB amazonDynamoDB)
		{
			this._amazonDynamoDB = amazonDynamoDB;
		}

		public List<TransactionCategory> GetAllCategories()
		{
			throw new NotImplementedException();
		}

		public TransactionCategory? NewTransactionCategory(TransactionCategory model)
		{
			throw new NotImplementedException();
		}

		public void RemoveTransactionCategory(string categoryName)
		{
			throw new NotImplementedException();
		}

		public Transaction? SaveNewTransaction(Transaction model)
		{
			throw new NotImplementedException();
		}

		public Transaction? ChangeTransaction(Transaction model)
		{
			throw new NotImplementedException();
		}
	}
}
