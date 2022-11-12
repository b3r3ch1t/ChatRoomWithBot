

using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Log.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatRoomWithBot.Log.IoC
{
    public static class RegisterLogDependency 
    {

        public static IServiceCollection RegisterLogDependencies(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment env)
        {

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
