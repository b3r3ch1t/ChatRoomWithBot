using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Services.BerechitLog.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatRoomWithBot.Services.BerechitLog.IoC
{
    public static class RegisterLogDependency 
    {

        public static IServiceCollection RegisterLogDependencies(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment env)
        {

            services.AddScoped<IBerechitLogger,BerechitLog>(); 


            var sentryDsn = configuration["SentryDsn"];

            if (!string.IsNullOrEmpty(sentryDsn) && (env.IsStaging() || env.IsProduction()))
            {
                services.AddSerilogApiSentry(sentryDsn);

            }
            else
            {
                services.AddSerilogApiConsole();
            }


            return services;
        }

    }
}
