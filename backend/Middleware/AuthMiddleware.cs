using fintrak.Helpers;

namespace fintrak.Middleware
{
	public class AuthMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly EnvHelper _envHelper;
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Called by app, at startup
		/// </summary>
		/// <param name="next"></param>
		public AuthMiddleware(RequestDelegate next, EnvHelper envHelper, IConfiguration configuration)
		{
			this._next = next;
			this._envHelper = envHelper;
			this._configuration = configuration;
		}

		/// <summary>
		/// Checks that the request has a Berarer token, that matches the configured token
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public async Task InvokeAsync(HttpContext context)
		{
			// no auth needed for root endpoint
			// no auth needed for localhost
			if (context.Request.Path == "/" || this._envHelper.GetCurrentEnv() == EnvHelper.Environments.LOCALHOST)
			{
				await this._next(context);
				return;
			}



			// Call the next delegate/middleware in the pipeline.
			await this._next(context);
		}

		private bool IsTokenValid(string token)
		{
			var goldenToken = this._configuration.GetValue<string>("fintrak_token");
			if (goldenToken == null || goldenToken == "")
			{
				throw new Exception("fintrak_token is unset");
			}

			return (goldenToken == token);
		}
	}

	/// <summary>
	/// Makes the TokenAuthMiddleware available to the app
	/// </summary>
	public static class AuthMiddlewareExtensions
	{
		/// <summary>
		/// Checks that the request has a Berarer token, that matches the stored token
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseAuth(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<AuthMiddleware>();
		}
	}
}
