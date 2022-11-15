using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces;
using MassTransit;

namespace ChatRoomWithBot.Services.RabbitMq.Consumers
{
    internal class ChatResponseCommandEventConsumer : IConsumer<ChatResponseCommandEvent>
    {
        private readonly IBerechitLogger _logger;
        private readonly IMediatorHandler _mediatorHandler;

        public ChatResponseCommandEventConsumer(IBerechitLogger logger, IMediatorHandler mediatorHandler)
        {
            _logger = logger;
            _mediatorHandler = mediatorHandler;
        }

        public  async Task Consume(ConsumeContext<ChatResponseCommandEvent> context)
        {
            try
            {
                var chatResponseCommandEvent = new ChatResponseCommandEvent()
                {
                    CodeRoom = context.Message.CodeRoom,
                    Message = context.Message.Message ,
                    UserId = Guid.Empty,
                    UserName = "bot"

                };

               await  _mediatorHandler.SendMessage(chatResponseCommandEvent); 
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
             
        }
    }
}
