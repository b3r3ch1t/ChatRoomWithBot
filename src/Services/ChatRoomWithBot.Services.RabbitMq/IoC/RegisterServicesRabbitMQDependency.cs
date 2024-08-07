using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Services.RabbitMq.Consumers;
using ChatRoomWithBot.Services.RabbitMq.Handler;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoomWithBot.Services.RabbitMq.IoC
{
    public static class RegisterServicesRabbitMqDependency
    {

        public static IServiceCollection RegisterServicesRabbitMqDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IRequestHandler<ChatMessageCommandEvent, CommandResponse>, BotMessageNotificationHandler>();

            var host = configuration.GetSection("RabbitMQ:Connection:HostName").Value;

            var username = configuration.GetSection("RabbitMQ:Connection:Username").Value;
            var password = configuration.GetSection("RabbitMQ:Connection:Password").Value;
            var receiveEndpoint = configuration.GetSection("RabbitMQ:botChatQueue").Value; 



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
