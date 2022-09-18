using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using fintrak.Data.Models;
using fintrak.Helpers;
using System.Net;

namespace fintrak.Data.Providers
{
    public class SystemDbProvider
    {

        private readonly IAmazonDynamoDB _dynamoDB;
        private readonly DbHelper _dbHelper;

        public SystemDbProvider(DbHelper dbHelper, IAmazonDynamoDB dynamoDB)
        {
            _dbHelper = dbHelper;
            _dynamoDB = dynamoDB;
        }

        public async Task<(string, DateTime)> NewSession()
        {
            var model = new Session();
            model.Guid = Guid.NewGuid().ToString();
            model.Expires = DateTime.Now.AddDays(1);

            var createRequest = new PutItemRequest
            {
                TableName = this._dbHelper.DbTableName,
                Item = this._dbHelper.ModelToItem(model)
            };

            var response = await this._dynamoDB.PutItemAsync(createRequest);
            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new Exception("Failed to create session");

            return (model.Guid, model.Expires);
        }

        public async Task<bool> IsSessionValid(string sessionId)
        {
            var getRequest = new GetItemRequest
            {
                TableName = this._dbHelper.DbTableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {"pk", new AttributeValue{ S = $"SESSION@{sessionId}" } },
                    {"sk", new AttributeValue{ S = $"SESSION@{sessionId}" } }
                }
            };

            var response = await this._dynamoDB.GetItemAsync(getRequest);
            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new Exception("Failed to query session");

            return response.Item.Count != 0;
        }

        public async void ClearSession(string sessionId)
        {
			var deleteRequest = new DeleteItemRequest
			{
				TableName = this._dbHelper.DbTableName,
				Key = new Dictionary<string, AttributeValue>
				{
					{"pk", new AttributeValue{ S = $"SESSION@{sessionId}" } },
					{"sk", new AttributeValue{ S = $"SESSION@{sessionId}" } }
				}
			};

			var response = await this._dynamoDB.DeleteItemAsync(deleteRequest);
			if (response.HttpStatusCode != HttpStatusCode.OK)
				throw new Exception("Failed to delete transaction");
        }

        public async void ClearSessions()
        {
            var filterExpression = "contains (pk, :id_prefix)";
            var filterValues = new Dictionary<string, AttributeValue> { { ":id_prefix", new AttributeValue { S = "SESSION@" } } };

            var scanRequest = new ScanRequest
            {
                TableName = this._dbHelper.DbTableName,
                FilterExpression = filterExpression,
                ExpressionAttributeValues = filterValues
            };

            var response = await this._dynamoDB.ScanAsync(scanRequest);
            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new Exception("Failed to delete sessions");

            foreach (var item in response.Items)
            {
                var session = this._dbHelper.ItemToModel<Session>(item);

                if (session != null)
                    this.ClearSession(session.Guid);
            }
        }
    }
}
