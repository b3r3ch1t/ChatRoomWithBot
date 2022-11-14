using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces;
using MediatR;

namespace ChatRoomWithBot.Domain.Bus
{
    internal class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IBerechitLogger _berechitLogger;


        public MediatorHandler(IMediator mediator, IBerechitLogger berechitLogger)
        {
            _mediator = mediator;
            _berechitLogger = berechitLogger;
        }

        public async Task<CommandResponse> SendMessage<T>(T chatMessage) where T : Event
        {
            try
            {
                await _mediator.Send(  chatMessage);
                return CommandResponse.Ok();
            }
            catch (Exception e)
            {
                _berechitLogger.Error(e);
                return CommandResponse.Fail(e);
            }

        }


    }
}
