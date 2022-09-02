using fintrak.Data;
using Microsoft.EntityFrameworkCore;
using fintrak.Middleware;
using fintrak.Helpers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Set up environment variable config provider
// env vars to configure are:
// - optional on localhost
// 		- fintrak_dbserver
// 		- fintrak_dbuser
// 		- fintrak_dbpassword
//		- fintrak_token
// - required everywhere
// 		- fintrak_envname

builder.Configuration.AddEnvironmentVariables("fintrak_");
string EnvVar(string key, string defaultValue) => builder?.Configuration.GetValue<string>(key) ?? defaultValue;

// Configure services
builder.Services.AddControllers();
builder.Services.AddTransient<DbProvider>();
builder.Services.AddTransient<EnvHelper>();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

// Cofigure DB
string dbServer = EnvVar("fintrak_dbserver", "localhost");
string dbUser = EnvVar("fintrak_dbuser", "root");
string dbPassword = EnvVar("fintrak_dbpassword", "rozsomak");

var connectionString = $"Server={dbServer};Port=3306;Database=fintrak;User={dbUser};Password={dbPassword};";
builder.Services.AddDbContext<AppDbContext>(options =>
{
	try
	{
		options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
	}
	catch(Exception)
	{
		// TODO: generate descriptive log, so if this fails on Lambda we can know
		throw;
	}
});

// Build app
var app = builder.Build();
if(EnvVar("fintrak_envname", "") == "localhost")
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// Configure middleware pipeline
app.UseHttpsRedirection();
app.UseAuth();
app.UseAuthorization();
app.MapControllers();

// Start app
app.Run();

// TODO add global error handler
