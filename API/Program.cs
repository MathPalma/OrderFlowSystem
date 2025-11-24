using API.AppConfig;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Host.UseNLog();


// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("OrderProcessingDb"));
builder.Services.AddRepositories();
builder.Services.AddRabbitMQ();
builder.Services.AddServices();

//Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OrderFlowSystem API",
        Version = "v1",
        Description = "Api for orders management"
    });
});

builder.Services.AddControllers();

var app = builder.Build();

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

logger.Info("Application started.");
logger.Warn("Database connection string: {ConnectionString}", builder.Configuration.GetConnectionString("OrderProcessingDb"));

//Configure swagger

app.UseSwagger();
    app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
