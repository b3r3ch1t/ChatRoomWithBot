using ChatRoomWithBot.Data.IoC;
using ChatRoomWithBot.Domain.IoC;
using ChatRoomWithBot.Service.Identity.IoC;
using ChatRoomWithBot.Services.Log.IoC;
using ChatRoomWithBot.Services.RabbitMq.Extensions;
using ChatRoomWithBot.Services.RabbitMq.IoC;
using ChatRoomWithBot.Services.RabbitMq.Settings;
using ChatRoomWithBot.UI.MVC.Extensions;
using System.Reflection;
using ChatRoomWithBot.Application.IoC;
using MediatR;
using ChatRoomWithBot.Application.AutoMapper;
using ChatRoomWithBot.UI.MVC.Interfaces;

const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";


var builder = WebApplication.CreateBuilder(args);


var type = typeof(IRoomController);
var types = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => type.IsAssignableFrom(p) && p.FullName != "ChatRoomWithBot.UI.MVC.Interfaces.IRoomController")
    .ToDictionary(x=>   x.GUID , x=> x.FullName);

var lisRooms = new List<Guid>();

foreach (var t in types)
{
     
    
}
    
 


builder.Services
    .RegisterDomainDependencies()
    .RegisterLogDependencies(builder.Configuration, builder.Environment)
    .RegisterApplicationDependencies(builder.Configuration)
    .RegisterDataDependencies(builder.Configuration)
    .RegisterServicesRabbitMqDependencies()
    .RegisterIdentityDependencies();


#region Mediator

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

#endregion



#region AutoMapper

builder.Services.AddAutoMapperSetup();

#endregion

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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();
 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Configure RabbitMQ

app.UseRabbitListener();

app.SeedData();

app.Run();


