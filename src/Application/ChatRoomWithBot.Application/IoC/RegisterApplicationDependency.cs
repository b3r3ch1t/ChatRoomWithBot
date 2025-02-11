using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoomWithBot.Application.IoC
{
    public static class RegisterApplicationDependency
    {
        public static IServiceCollection RegisterApplicationDependencies(
            this IServiceCollection services )
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUsersAppService, UsersAppService>();

            services.AddScoped<IChatManagerApplication, ChatManagerApplication>(); 

            return services;

        }
    }
}
