using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection; 
namespace ChatRoomWithBot.Service.SignalR.IoC
{
    public static  class RegisterSignalRDependency
    {
        public static IServiceCollection RegisterSignalRDependencies(
            this IServiceCollection services)
        {

            services.AddScoped<IChatManagerSignalR, ChatManagerSignalR>();
            
            return services;
        }

    }
}
