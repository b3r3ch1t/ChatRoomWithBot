using ChatRoomWithBot.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ChatRoomWithBot.Data.Interfaces;
using ChatRoomWithBot.Data.Repository;
using ChatRoomWithBot.Domain.Interfaces.Repositories;

namespace ChatRoomWithBot.Data.IoC
{
    public static class RegisterDataDependency
    {
        public static IServiceCollection RegisterDataDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {


            services.AddTransient<DataSeeder>();



            services.AddScoped<IUserIdentityRepository, UserIdentityRepository>();
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();

            //services.AddDbContext<ChatRoomWithBotContext>(
            //    opt => opt
            //        .UseInMemoryDatabase("ChatRoomWithBotContext"));

            services.AddDbContext<ChatRoomWithBotContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));



            return services;
        }
    }
}
