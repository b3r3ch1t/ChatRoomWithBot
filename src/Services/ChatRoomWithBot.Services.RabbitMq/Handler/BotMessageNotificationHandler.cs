using ChatRoomWithBot.Domain.Commands;
using ChatRoomWithBot.Domain.Interfaces;
using MediatR;

namespace ChatRoomWithBot.Services.RabbitMq.Handler
{
    internal class BotMessageNotificationHandler : INotificationHandler<BotMessageEvent>
    {

        
        public Task Handle(BotMessageEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
