using fintrak.Middleware;
using fintrak.Helpers;
using System.Reflection;
using fintrak.Data.Providers;
using Amazon.DynamoDBv2;

var builder = WebApplication.CreateBuilder(args);

// Set up environment variable config provider
builder.Configuration.AddEnvironmentVariables("fintrak_");
string EnvVar(string key, string defaultValue) => builder?.Configuration.GetValue<string>(key) ?? defaultValue;

string dynamoDbAccessKey = EnvVar("fintrak_dynamoaccess", "");
string dynamoDbSecretKey = EnvVar("fintrak_dynamosecret", "");
if (String.IsNullOrEmpty(dynamoDbSecretKey) || String.IsNullOrEmpty(dynamoDbAccessKey))
    throw new Exception("fintrak_dynamoaccess or fintrak_dynamosecret is not set");

// Configure services
builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.AddSingleton<IAmazonDynamoDB>(_ =>
    new AmazonDynamoDBClient(
        dynamoDbAccessKey,
        dynamoDbSecretKey,
        Amazon.RegionEndpoint.USEast1
    )
);

builder.Services.AddSingleton<TransactionDbProvider>();
builder.Services.AddSingleton<ReportDbProvider>();
builder.Services.AddSingleton<SystemDbProvider>();

builder.Services.AddSingleton<EnvHelper>();
builder.Services.AddSingleton<DbHelper>();

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

// Configure middleware pipeline
app.UseCors(x =>
    x.WithOrigins("http://localhost:4200", "https://fintrak.blaczko.com")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
app.UseHttpsRedirection();
app.UseAuth();
app.MapControllers();

if (EnvVar("fintrak_envname", "").ToUpper() == "LOCALHOST")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Start app
app.Run();

// TODO add global error handler
