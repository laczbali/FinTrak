using fintrak.Data;
using fintrak.Middleware;
using fintrak.Helpers;
using System.Reflection;
using fintrak.Data.Providers;
using Amazon.DynamoDBv2;

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
builder.Services.AddSingleton<IAmazonDynamoDB>();
builder.Services.AddSingleton<BaseDbProvider>();
builder.Services.AddSingleton<ReportDbProvider>();
builder.Services.AddSingleton<EnvHelper>();

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
