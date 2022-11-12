using ChatRoomWithBot.Data.IoC;
using ChatRoomWithBot.Domain.IoC;
using ChatRoomWithBot.Log.IoC;
using ChatRoomWithBot.UI.MVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services
    .RegisterDomainDependencies()
    .RegisterLogDependencies(builder.Configuration, builder.Environment)
    .RegisterDataDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.SeedData();

app.Run();


