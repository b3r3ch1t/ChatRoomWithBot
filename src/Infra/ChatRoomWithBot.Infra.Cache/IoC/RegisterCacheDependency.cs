using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoomWithBot.Infra.Cache.IoC;

public static class RegisterCacheDependency
{

    public static IServiceCollection RegisterCacheDependencies(
        this IServiceCollection services )
    {
      
        services.AddScoped<ICacheRepository, CacheRepository>();
        
        return services;
    }

}