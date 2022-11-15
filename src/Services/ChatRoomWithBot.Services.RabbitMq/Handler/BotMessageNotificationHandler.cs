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

           
             
        }
    }
}
