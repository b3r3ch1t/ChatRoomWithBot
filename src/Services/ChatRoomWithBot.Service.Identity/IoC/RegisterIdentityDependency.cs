using ChatRoomWithBot.Service.Identity.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ChatRoomWithBot.Service.Identity.Services;
using Microsoft.AspNetCore.Identity;
using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Data.IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace ChatRoomWithBot.Service.Identity.IoC
{
    public static  class RegisterIdentityDependency
    {
        public static IServiceCollection RegisterIdentityDependencies(
            this IServiceCollection services )
        {

            services.AddScoped<IUserIdentityManager, UserIdentityManager>();

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

            
            

            services.AddDefaultIdentity<UserIdentity>(
                    options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ChatRoomWithBotContext>();


            // Add services to the container.
            //services.AddControllersWithViews();


            services.AddControllersWithViews(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });


            return services; 
        }
    }
}
