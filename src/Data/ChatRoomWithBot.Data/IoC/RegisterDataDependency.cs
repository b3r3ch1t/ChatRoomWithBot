using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Data.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ChatRoomWithBot.Data.Interfaces;
using ChatRoomWithBot.Data.Repository;

namespace ChatRoomWithBot.Data.IoC
{
    public static class RegisterDataDependency
    {
        public static IServiceCollection RegisterDataDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ChatRoomWithBotContext>(
                opt => opt
                    .UseInMemoryDatabase("ChatRoomWithBotContext"));

            services.AddTransient<DataSeeder>();

         

            services.AddScoped<IUserIdentityRepository, UserIdentityRepository>();

            return services;
        }
    }
}
