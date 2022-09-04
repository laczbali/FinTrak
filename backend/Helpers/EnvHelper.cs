namespace fintrak.Helpers
{
	public class EnvHelper
	{
		private readonly IConfiguration _configuration;
		public readonly EnvConfig Config;

		public EnvHelper(IConfiguration configuration)
		{
			this._configuration = configuration;

			string GetEnvVar(string key) => this._configuration.GetValue<string>(key);
			this.Config = new EnvConfig();

			// parse optional params
			this.Config.AuthToken = GetEnvVar("fintrak_token") ?? "";

			// parse required params
			var envnameVar = GetEnvVar("fintrak_envname") ?? "";
			var envnameVarSuccess = Enum.TryParse<Environments>(envnameVar.ToUpper(), out var env);
			if(!envnameVarSuccess) throw new Exception("fintrak_envname is unset, or incorrect");
			this.Config.CurrentEnvironment = env;

			this.Config.DbTableName = GetEnvVar("fintrak_dbtablename");
			if (Config.DbTableName == null) throw new Exception("fintrak_dbtablename is unset");

			this.Config.DynamoAccessKey = GetEnvVar("fintrak_dynamoaccess");
			if (Config.DynamoAccessKey == null) throw new Exception("fintrak_dynamoaccess is unset");

			this.Config.DynamoSecretKey = GetEnvVar("fintrak_dynamosecret");
			if (Config.DynamoSecretKey == null) throw new Exception("fintrak_dynamosecret is unset");
		}

		public enum Environments
		{
			LOCALHOST,
			LAMBDA
		}

		public class EnvConfig
		{
			public Environments CurrentEnvironment { get; set; }
			public string AuthToken { get; set; } = string.Empty;
			public string DbTableName { get; set; } = string.Empty;
			public string DynamoAccessKey { get; set; } = string.Empty;
			public string DynamoSecretKey { get; set; } = string.Empty;
		}
	}
}
