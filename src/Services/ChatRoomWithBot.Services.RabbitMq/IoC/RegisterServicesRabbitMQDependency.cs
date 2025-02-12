using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events; 
using ChatRoomWithBot.Services.RabbitMq.Handler;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoomWithBot.Services.RabbitMq.IoC
{
    public static class RegisterServicesRabbitMqDependency
    {

        public static IServiceCollection RegisterServicesRabbitMqDependencies(
            this IServiceCollection services )
        {
            services.AddScoped<IRequestHandler<ChatMessageCommandEvent, CommandResponse>, BotMessageNotificationHandler>();

            var host = SharedSettings.Current.RabbitMq.Host;

            var username = SharedSettings.Current.RabbitMq.Username ;
            var password = SharedSettings.Current.RabbitMq.Password;
            var receiveEndpoint = SharedSettings.Current.RabbitMq.Queue;

            if (string.IsNullOrWhiteSpace(receiveEndpoint)) return services;

           

            return services;
        }

    }
}
