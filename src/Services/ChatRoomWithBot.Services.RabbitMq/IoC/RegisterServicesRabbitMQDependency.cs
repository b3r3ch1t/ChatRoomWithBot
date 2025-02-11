using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Services.RabbitMq.Consumers;
using ChatRoomWithBot.Services.RabbitMq.Handler;
using MassTransit;
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

            services.AddMassTransit(x =>
            {

                x.AddConsumer<ChatResponseCommandEventConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    
                    config.Host(new Uri($"rabbitmq://{host}"), h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    config.ReceiveEndpoint(receiveEndpoint, ep =>
                    {
                        ep.ConfigureConsumer<ChatResponseCommandEventConsumer>(provider);
                    });


                }));
            });

            services.AddMassTransitHostedService();

            return services;
        }

    }
}
