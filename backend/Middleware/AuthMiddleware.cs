using fintrak.Data.Providers;
using fintrak.Helpers;

namespace fintrak.Middleware
{
	public class AuthMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly EnvHelper _envHelper;
        private readonly SystemDbProvider _dbProvider;

		/// <summary>
		/// Called by app, at startup
		/// </summary>
		public AuthMiddleware(RequestDelegate next, EnvHelper envHelper, SystemDbProvider dbProvider)
		{
			this._next = next;
			this._envHelper = envHelper;
            this._dbProvider = dbProvider;
        }

		/// <summary>
		/// Checks that the request has a Berarer token, that matches the configured token
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public async Task InvokeAsync(HttpContext context)
		{
			// no auth needed for root endpoint
			// no auth for login
			// no auth needed for localhost
			if (context.Request.Path == "/"
				|| context.Request.Path == "/auth/login"
				|| this._envHelper.Config.CurrentEnvironment == EnvHelper.Environments.LOCALHOST)
			{
				await this._next(context);
				return;
			}

			var requestIsValid = false;

			context.Request.Headers.TryGetValue("Authorization", out var authHeader);
			var authToken = authHeader.ToString().Replace("Bearer ", "");
			if (authToken != null)
			{
				requestIsValid = requestIsValid || IsTokenValid(authToken);
			}

			var authCookie = context.Request.Cookies.FirstOrDefault(x => x.Key == "Auth");
			if(authCookie.Key != null)
			{
				var sessionOK = await this._dbProvider.IsSessionValid(authCookie.Value);
				requestIsValid = requestIsValid || sessionOK;
			}

			if(requestIsValid)
			{
				// Call the next delegate/middleware in the pipeline.
				await this._next(context);
				return;
			}

			context.Response.StatusCode = 401;
			context.Response.Cookies.Delete("Auth");
			return;
		}

		private bool IsTokenValid(string token)
		{
			var goldenToken = this._envHelper.Config.AuthToken;
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
