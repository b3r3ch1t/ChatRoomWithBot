using ChatRoomWithBot.Domain.Interfaces;
using MediatR;

namespace ChatRoomWithBot.Domain.Bus
{
    internal class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IError _error;

        public MediatorHandler(IDependencyResolver dependencyResolver)
        {
            _mediator = dependencyResolver.Resolve<IMediator>();
            _error = dependencyResolver.Resolve<IError>();

        }
        public async  Task<CommandResponse> SendMessage<T>(T chatMessage) where T : Event
        {
            try
            {
                await _mediator.Publish(chatMessage);
                return CommandResponse.Ok();
            }
            catch (Exception e)
            {
                _error.Error(e);
                return CommandResponse.Fail(e);
            }
             
        }
    }
}
