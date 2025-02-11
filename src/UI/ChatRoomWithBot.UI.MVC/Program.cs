using ChatRoomWithBot.Domain.IoC;
using ChatRoomWithBot.Services.RabbitMq.Settings;
using MediatR; 
using ChatRoomWithBot.Application.IoC;
using ChatRoomWithBot.Data.IoC;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.UI.MVC.Services; 
using ChatRoomWithBot.Services.RabbitMq.IoC;
using ChatRoomWithBot.UI.MVC.Handles;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Services.BerechitLogger.IoC;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Azure.Identity;

const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
	.AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
	;


builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
	options.SignedOutRedirectUri = builder.Configuration["AzureAd:PostLogoutRedirectUri"];
});


var clientId = builder.Configuration.GetSection("AzureAd:clientId").Value;
var tenantId = builder.Configuration.GetSection("AzureAd:tenantId").Value ;
var clientSecret = builder.Configuration.GetSection("AzureAd:clientSecret").Value;

var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

var graphClient = new GraphServiceClient(credential);


builder.Services.AddScoped<GraphServiceClient>(x=> graphClient); 


builder.Services
	.RegisterDomainDependencies()
	.RegisterLogDependencies(builder.Configuration, builder.Environment)
	.RegisterApplicationDependencies(builder.Configuration)
	.RegisterDataDependencies(builder.Configuration)
	.RegisterServicesRabbitMqDependencies(builder.Configuration) ;


#region Mediator

builder.Services.AddMediatR(cfg => cfg
	.RegisterServicesFromAssembly(typeof(RegisterDomainDependency).Assembly));

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
		pattern: "{controller=Home}/{action=Index}/{id?}") ;


app.UseEndpoints(endpoints =>
{
	endpoints.MapHub<ChatRoomHub>("/chatroom");
});

//Configure RabbitMQ

//app.UseRabbitListener(); 

//app.SeedData();

app.Run();


