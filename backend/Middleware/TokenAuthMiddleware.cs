using fintrak.Helpers;

namespace fintrak.Middleware
{
	public class TokenAuthMiddleware
	{
        private readonly RequestDelegate _next;
		private readonly EnvHelper _envHelper;
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Called by app, at startup
		/// </summary>
		/// <param name="next"></param>
		public TokenAuthMiddleware(RequestDelegate next, EnvHelper envHelper, IConfiguration configuration)
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
            if (context.Request.Path == "/")
            {
                await this._next(context);
                return;
            }

            context.Request.Headers.TryGetValue("Authorization", out var authHeader);

            var authToken = authHeader.ToString().Replace("Bearer ", "");
            if (authToken == null || authToken == "")
            {
                // no token was provided
                context.Response.StatusCode = 401;
                return;
            }

            // check if the token is valid
            if (!IsTokenValid(authToken))
            {
                context.Response.StatusCode = 401;
                return;
            }

            // Call the next delegate/middleware in the pipeline.
            await this._next(context);
        }

        private bool IsTokenValid(string token)
		{
            if(this._envHelper.GetCurrentEnv() == EnvHelper.Environments.Localhost)
			{
                // no need to validate token on localhost
                return true;
			}

            var goldenToken = this._configuration.GetValue<string>("fintrak_token");
            if(goldenToken == null || goldenToken == "")
			{
                throw new Exception("Auth token is unset");
			}

            return (goldenToken == token);
		}
	}

    /// <summary>
    /// Makes the TokenAuthMiddleware available to the app
    /// </summary>
    public static class TokenAuthMiddlewareExtensions
    {
        /// <summary>
        /// Checks that the request has a Berarer token, that matches the stored token
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseTokenAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenAuthMiddleware>();
        }
    }
}
