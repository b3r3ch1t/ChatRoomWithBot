using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Services.RabbitMq.Handler;
using ChatRoomWithBot.Services.RabbitMq.Manager;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoomWithBot.Services.RabbitMq.IoC
{
    public static class RegisterServicesRabbitMqDependency
    {

        public static IServiceCollection RegisterServicesRabbitMqDependencies(
            this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<ChatMessageCommandEvent, CommandResponse>, BotMessageNotificationHandler>();
            services.AddScoped<IRabbitMqManager, RabbitMqManager>();


            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.UseHealthCheck(provider);
                    config.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                }));
            });

            services.AddMassTransitHostedService();

            return services;
        }

    }
}
