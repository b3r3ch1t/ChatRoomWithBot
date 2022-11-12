using ChatRoomWithBot.Domain.Commands;
using ChatRoomWithBot.Domain.Interfaces;
using MediatR;

namespace ChatRoomWithBot.Services.RabbitMq
{
    internal class BotMessageNotificationHandler : INotificationHandler<BotMessageEvent>
    {

        protected BotMessageNotificationHandler(IDependencyResolver dependencyResolver) 
        {

        }
        public Task Handle(BotMessageEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
