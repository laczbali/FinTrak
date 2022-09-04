﻿using fintrak.Helpers;

namespace fintrak.Middleware
{
	public class AuthMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly EnvHelper _envHelper;

		/// <summary>
		/// Called by app, at startup
		/// </summary>
		public AuthMiddleware(RequestDelegate next, EnvHelper envHelper)
		{
			this._next = next;
			this._envHelper = envHelper;
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
			if (context.Request.Path == "/" || this._envHelper.Config.CurrentEnvironment == EnvHelper.Environments.LOCALHOST)
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
