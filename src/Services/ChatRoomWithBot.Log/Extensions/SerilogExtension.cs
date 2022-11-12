using Microsoft.Extensions.Configuration;
using Serilog.Events;
using Serilog.Filters;
using Serilog;
using System;
using Serilog.Core;
using Serilog.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoomWithBot.Log.Extensions
{
    internal static class SerilogExtension
    {

        public static void AddSerilogApiConsole(this IServiceCollection configuration)
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                  .Enrich.FromLogContext()
                  .Enrich.WithExceptionDetails()
                  .Enrich.WithCorrelationId()
                  .Enrich.WithProperty("ApplicationName", $"API Serilog - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
                  .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                  .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("Business error"))
                  .WriteTo.Async(wt => wt.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                  .CreateLogger();
        }


        public static void AddSerilogApiSentry(this IServiceCollection configuration, string sentryDns)
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithProperty("ApplicationName", $"API Serilog - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("Business error"))
                .WriteTo.Async(wt => wt.Sentry(sentryDns))
                .CreateLogger();
        }
    }
}
