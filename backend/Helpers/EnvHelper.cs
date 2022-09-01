namespace fintrak.Helpers
{
	public class EnvHelper
	{
		private readonly IConfiguration _configuration;

		public EnvHelper(IConfiguration configuration)
		{
			this._configuration = configuration;
		}

		public Environments GetCurrentEnv()
		{
			var envnameVar = _configuration.GetValue<string>("fintrak_envname") ?? "";
			var success = Enum.TryParse<Environments>(envnameVar, out var env);

			if(success) return env;
			return Environments.Unset;

		}

		public enum Environments
		{
			Unset,
			Localhost,
			Lambda
		}
	}
}
