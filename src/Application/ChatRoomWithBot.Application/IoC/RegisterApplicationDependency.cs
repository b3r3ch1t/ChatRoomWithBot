﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoomWithBot.Application.IoC
{
    public static class RegisterApplicationDependency
    {
        public static IServiceCollection RegisterApplicationDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;

        }
    }
}
