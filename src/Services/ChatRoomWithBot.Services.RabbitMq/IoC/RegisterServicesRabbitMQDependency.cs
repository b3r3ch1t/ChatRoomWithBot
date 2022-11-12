using ChatRoomWithBot.Domain.Commands;
using ChatRoomWithBot.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoomWithBot.Services.RabbitMq.IoC
{
    public static class RegisterServicesRabbitMqDependency
    {

        public static IServiceCollection RegisterServicesRabbitMqDependencies(
            this IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<BotMessageEvent>, BotMessageNotificationHandler>();
            services.AddScoped<IRabbitMqManager, RabbitMqManager>();


            return services;
        }

    }
}
