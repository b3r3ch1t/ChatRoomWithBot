using ChatRoomWithBot.Domain.IoC;
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
using Azure.Identity;
using System.Reflection;
using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Infra.HttpRequest.Infra.HttpRequest.IoC;
using ChatRoomWithBot.UI.MVC;
using ChatRoomWithBot.UI.MVC.BackgroundServices;
using RabbitMQ.Client;


Utils.AppName = Assembly.GetExecutingAssembly().GetName().Name;

Console.WriteLine($"AppName: {Utils.AppName}");
Console.WriteLine($"Checking UTC time: {Utils.Now} - Utils.NowBr: {Utils.NowBr}");
Console.WriteLine("Current Environment: " + Utils.EnvironmentName);





var builder = WebApplication.CreateBuilder(args);




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


// Add services to the container.
builder.Services.AddControllersWithViews();

#region HealChecks


builder.Services.AddSingleton<IConnection>(sp =>
{

    var factory = new ConnectionFactory()
    {
        HostName = SharedSettings.Current.RabbitMq.Host,
        Port = SharedSettings.Current.RabbitMq.Port,
        UserName = SharedSettings.Current.RabbitMq.Username,
        Password = SharedSettings.Current.RabbitMq.Password
    };




    return factory.CreateConnection();
});


builder.Services.AddHealthChecks()
    .AddSqlServer(sharedSettings.SQLServer.ConnectionString)
    //.AddRabbitMQ()
    
    ;

#endregion


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
    .RegisterHttpRequestDependencies();


#region Mediator

builder.Services.AddMediatR(cfg => cfg
    .RegisterServicesFromAssembly(typeof(RegisterDomainDependency).Assembly));

#endregion


builder.Services
    .AddHostedService<Worker>();

builder.Services.AddScoped<IProcessarChatMessageCommandEvent, ProcessarChatMessageCommandEvent>();




builder.Services.AddSignalR();

builder.Services.AddScoped<IRequestHandler<ChatMessageTextEvent, CommandResponse>, ChatRoomHandler>();
builder.Services.AddScoped<IRequestHandler<ChatResponseCommandEvent, CommandResponse>, ChatRoomHandler>();



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


app.MapHealthChecks("/health");


app.Run();


