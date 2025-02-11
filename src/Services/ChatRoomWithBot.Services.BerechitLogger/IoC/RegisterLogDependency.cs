using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Services.BerechitLogger.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoomWithBot.Services.BerechitLogger.IoC
{
    public static class RegisterLogDependency 
    {

        public static IServiceCollection RegisterLogDependencies(
            this IServiceCollection services)
        {

            services.AddScoped<IBerechitLogger,BerechitLog>();


            var sentryDsn = SharedSettings.Current.Sentry; 


            if (!string.IsNullOrEmpty(sentryDsn) && ( Utils.IsStagingEnvironment || Utils.IsProductionEnvironment ))
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
