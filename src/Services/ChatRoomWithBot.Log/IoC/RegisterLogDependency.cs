

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoomWithBot.Log.IoC
{
    public static class RegisterLogDependency
    {

        public static IServiceCollection RegisterLogDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {



            return services;
        }

    }
}
