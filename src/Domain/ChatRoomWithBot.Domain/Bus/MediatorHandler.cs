using ChatRoomWithBot.Domain.Interfaces;
using MediatR;

namespace ChatRoomWithBot.Domain.Bus
{
    internal class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IError _error;


        public MediatorHandler(IMediator mediator, IError error)
        {
            _mediator = mediator;
            _error = error;
        }

        public async  Task<CommandResponse> SendMessage<T>(T chatMessage) where T : Event
        {
            try
            {
                await _mediator.Send(chatMessage);
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
