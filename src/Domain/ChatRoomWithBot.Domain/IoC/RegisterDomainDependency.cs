 

using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoomWithBot.Domain.IoC
{
    public static  class RegisterDomainDependency
    {
        public static IServiceCollection RegisterDomainDependencies(
            this IServiceCollection services )
        {

            services.AddScoped<IDependencyResolver, DependencyResolver>();


            return services;
        }
    }
}
