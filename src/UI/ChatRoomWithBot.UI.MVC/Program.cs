using ChatRoomWithBot.Data.IoC;
using ChatRoomWithBot.Domain.IoC;
using ChatRoomWithBot.Service.Identity.IoC;
using ChatRoomWithBot.Services.Log.IoC;
using ChatRoomWithBot.Services.RabbitMq.Extensions;
using ChatRoomWithBot.Services.RabbitMq.IoC;
using ChatRoomWithBot.Services.RabbitMq.Settings;
using ChatRoomWithBot.UI.MVC.Extensions;

const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services
    .RegisterDomainDependencies()
    .RegisterLogDependencies(builder.Configuration, builder.Environment)
    .RegisterDataDependencies(builder.Configuration)
    .RegisterServicesRabbitMqDependencies()
    .RegisterIdentityDependencies();


builder.Services.Configure<RabbitMqSettings>(
    builder.Configuration.GetSection("RabbitMQ"));



var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.Local.json", optional: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable(AspNetCoreEnvironment)}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Configure RabbitMQ

app.UseRabbitListener();

app.SeedData();

app.Run();


