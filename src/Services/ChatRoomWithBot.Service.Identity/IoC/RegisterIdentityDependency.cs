using ChatRoomWithBot.Service.Identity.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ChatRoomWithBot.Service.Identity.Services;

namespace ChatRoomWithBot.Service.Identity.IoC
{
    public static  class RegisterIdentityDependency
    {
        public static IServiceCollection RegisterIdentityDependencies(
            this IServiceCollection services )
        {

            services.AddScoped<IUserIdentityManager, UserIdentityManager>();


            return services; 
        }
    }
}
