using fintrak.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
string EnvVar(string key, string defaultValue) => builder?.Configuration.GetValue<string>(key) ?? defaultValue;

// Configure services
builder.Services.AddControllers();

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

// Configure middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Start app
app.Run();
