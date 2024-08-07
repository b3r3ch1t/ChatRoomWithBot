using ChatRoomWithBot.Domain.IoC;
using ChatRoomWithBot.Services.RabbitMq.Settings;
using ChatRoomWithBot.UI.MVC.Extensions;
using System.Reflection;
using MediatR;
using ChatRoomWithBot.Application.AutoMapper;
using ChatRoomWithBot.Application.IoC;
using ChatRoomWithBot.Data.IoC;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.UI.MVC.Services;
using ChatRoomWithBot.Service.Identity.IoC;
using ChatRoomWithBot.Services.RabbitMq.IoC;
using ChatRoomWithBot.UI.MVC.Handles;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Services.BerechitLogger.IoC;

const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";


var builder = WebApplication.CreateBuilder(args);



builder.Services
	.RegisterDomainDependencies()
	.RegisterLogDependencies(builder.Configuration, builder.Environment)
	.RegisterApplicationDependencies(builder.Configuration)
	.RegisterDataDependencies(builder.Configuration)
	.RegisterServicesRabbitMqDependencies(builder.Configuration)
	.RegisterIdentityDependencies();


#region Mediator

builder.Services.AddMediatR(cfg => cfg
	.RegisterServicesFromAssembly(typeof(RegisterDomainDependency).Assembly));

#endregion


#region AutoMapper

builder.Services.AddAutoMapperSetup();

#endregion

builder.Services.Configure<RabbitMqSettings>(
	builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddSignalR();

builder.Services.AddScoped<IRequestHandler<ChatMessageTextEvent, CommandResponse>, ChatRoomHandler>();
builder.Services.AddScoped<IRequestHandler<ChatResponseCommandEvent, CommandResponse>, ChatRoomHandler>();


new ConfigurationBuilder()
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


app.UseEndpoints(endpoints =>
{
	endpoints.MapHub<ChatRoomHub>("/chatroom");
});

//Configure RabbitMQ

//app.UseRabbitListener(); 

app.SeedData();

app.Run();


