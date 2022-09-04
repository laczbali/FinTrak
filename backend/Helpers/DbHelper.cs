using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using System.Text.Json;

namespace fintrak.Helpers
{
	public class DbHelper
	{
		private readonly EnvHelper _envHelper;
		public string DbTableName => this._envHelper.Config.DbTableName;

		public DbHelper(EnvHelper envHelper)
		{
			this._envHelper = envHelper;
		}

		public Dictionary<string, AttributeValue>? ModelToItem(object model)
		{
			var modelJson = JsonSerializer.Serialize(model);
			var modelDocument = Document.FromJson(modelJson);
			var modelItem = modelDocument.ToAttributeMap();
			return modelItem;
		}

		public T? ItemToModel<T>(Dictionary<string, AttributeValue> responseItem)
		{
			var itemDocument = Document.FromAttributeMap(responseItem);
			return JsonSerializer.Deserialize<T>(itemDocument.ToJson());
		}
	}
}
