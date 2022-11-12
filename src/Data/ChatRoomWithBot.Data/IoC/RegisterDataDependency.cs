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

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8; //Valor default: 6
                options.Password.RequiredUniqueChars = 1; //Valor default = 1
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(6);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            });


            services.AddIdentity<UserIdentity, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ChatRoomWithBotContext>();



            services.AddScoped<IUserIdentityRepository, UserIdentityRepository>();

            return services;
        }
    }
}
