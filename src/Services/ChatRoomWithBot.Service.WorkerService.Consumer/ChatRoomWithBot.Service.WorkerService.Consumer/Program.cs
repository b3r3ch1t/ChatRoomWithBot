using ChatRoomWithBot.Service.WorkerService.Consumer;
using System.Reflection;
using Azure.Identity;
using ChatRoomWithBot.Application.IoC;
using ChatRoomWithBot.Data.IoC;
using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Domain.IoC;
using ChatRoomWithBot.Infra.Cache.IoC;
using ChatRoomWithBot.Infra.HttpRequest.Infra.HttpRequest.IoC;
using ChatRoomWithBot.Services.BerechitLogger.IoC;
using ChatRoomWithBot.Services.RabbitMq.IoC;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using ChatRoomWithBot.Service.WorkerService.Consumer.IoC;


Utils.AppName = Assembly.GetExecutingAssembly().GetName().Name;

Console.WriteLine($"AppName: {Utils.AppName}");
Console.WriteLine($"Checking UTC time: {Utils.Now} - Utils.NowBr: {Utils.NowBr}");
Console.WriteLine("Current Environment: " + Utils.EnvironmentName);






var builder = Host.CreateApplicationBuilder(args);




if (Utils.IsStagingEnvironment || Utils.IsProductionEnvironment)
{
    builder.Configuration.AddEnvironmentVariables();
}

if (Utils.IsDevelopmentEnvironment)
{
    builder.Configuration.AddUserSecrets<Program>();
}



var sharedSettings = new SharedSettings();


builder.Configuration.Bind("SharedSettings", sharedSettings);


#region Microsoft Entra ID
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("SharedSettings:AzureAd"))
    ;


builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.SignedOutRedirectUri = sharedSettings.AzureAd.PostLogoutRedirectUri;
});

#endregion

#region Graph


var clientId = sharedSettings.AzureAd.ClientId;
var tenantId = sharedSettings.AzureAd.TenantId;
var clientSecret = sharedSettings.AzureAd.ClientSecret;

var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

var graphClient = new GraphServiceClient(credential);


builder.Services.AddScoped<GraphServiceClient>(x => graphClient);


#endregion

builder.Services
    .RegisterDomainDependencies()
    .RegisterLogDependencies()
    .RegisterApplicationDependencies()
    .RegisterDataDependencies()
    .RegisterServicesRabbitMqDependencies()
    .RegisterCacheDependencies()
    .RegisterConsumerDependencies();


#region Mediator

builder.Services.AddMediatR(cfg => cfg
    .RegisterServicesFromAssembly(typeof(RegisterDomainDependency).Assembly));

#endregion

builder.Services.AddSignalR();


builder.Services.AddHostedService<Worker>();

var host = builder.Build();


host.Run();
