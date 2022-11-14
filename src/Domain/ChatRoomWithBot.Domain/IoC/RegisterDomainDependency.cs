 

using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Domain.Services;
using ChatRoomWithBot.Domain.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoomWithBot.Domain.IoC
{
    public static  class RegisterDomainDependency
    {
        public static IServiceCollection RegisterDomainDependencies(
            this IServiceCollection services )
        {

      
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IChatManagerDomain, ChatManagerDomain>();
            services.AddScoped<EventValidator>();
            

            return services;
        }
    }
}
