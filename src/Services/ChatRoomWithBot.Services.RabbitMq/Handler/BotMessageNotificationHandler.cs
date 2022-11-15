using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Services.RabbitMq.Settings;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Options; 

namespace ChatRoomWithBot.Services.RabbitMq.Handler
{
    internal class BotMessageNotificationHandler : IRequestHandler<ChatMessageCommandEvent, CommandResponse>
    {

        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly IBerechitLogger _berechitLogger;
        private readonly IBus _bus;
        public BotMessageNotificationHandler(IOptions<RabbitMqSettings> rabbitMqSettings, IBerechitLogger berechitLogger, IBus bus)
        {
            _berechitLogger = berechitLogger;
            _bus = bus;
            _rabbitMqSettings = rabbitMqSettings.Value;
        }

        public async Task<CommandResponse> Handle(ChatMessageCommandEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var teste = JsonSerializer.Serialize(notification);
                var uri = new Uri("rabbitmq://localhost/botBundleQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(notification);
                return CommandResponse.Ok();
            }
            catch (Exception e)
            {
                return CommandResponse.Fail(e);
            }


        }
    }
}
