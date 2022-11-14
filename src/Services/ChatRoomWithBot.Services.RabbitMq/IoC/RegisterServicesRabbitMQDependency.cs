using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Services.RabbitMq.Handler;
using ChatRoomWithBot.Services.RabbitMq.Manager;
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
