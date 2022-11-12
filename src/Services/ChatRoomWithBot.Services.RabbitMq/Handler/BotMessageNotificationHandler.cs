using ChatRoomWithBot.Domain.Commands;
using ChatRoomWithBot.Domain.Interfaces;
using MediatR;

namespace ChatRoomWithBot.Services.RabbitMq.Handler
{
    internal class BotMessageNotificationHandler : INotificationHandler<BotMessageEvent>
    {

        public  BotMessageNotificationHandler(IDependencyResolver dependencyResolver)
        {

        }
        public Task Handle(BotMessageEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
