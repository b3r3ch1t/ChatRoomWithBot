using System.Text;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Services.RabbitMq.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ChatRoomWithBot.Services.RabbitMq.Handler
{
    internal class BotMessageNotificationHandler : IRequestHandler<ChatMessageCommandEvent, CommandResponse>
    {

        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly IBerechitLogger _berechitLogger;

        public BotMessageNotificationHandler(IOptions<RabbitMqSettings> rabbitMqSettings, IBerechitLogger berechitLogger)
        {
            _berechitLogger = berechitLogger;
            _rabbitMqSettings = rabbitMqSettings.Value;
        }

        public async  Task<CommandResponse> Handle(ChatMessageCommandEvent notification, CancellationToken cancellationToken)
        {

            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _rabbitMqSettings.Connection.HostName,
                    UserName = _rabbitMqSettings.Connection.Username,
                    Password = _rabbitMqSettings.Connection.Password
                };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(
                    queue: _rabbitMqSettings.BotBundleQueue.Name,
                    durable: _rabbitMqSettings.BotBundleQueue.Durable,
                    exclusive: _rabbitMqSettings.BotBundleQueue.Exclusive,
                    autoDelete: _rabbitMqSettings.BotBundleQueue.AutoDelete,
                    arguments: null);


                var msg = System.Text.Json.JsonSerializer.Serialize(notification); 

                var body = Encoding.UTF8.GetBytes(msg);

                channel.BasicPublish(exchange: "",
                    routingKey: _rabbitMqSettings.BotBundleQueue.Name,
                    basicProperties: null,
                    body: body);

                return CommandResponse.Ok();
            }
            catch (Exception e)
            {
                _berechitLogger.Error(e);
                return CommandResponse.Fail( e);
            }

             
        }
    }
}
