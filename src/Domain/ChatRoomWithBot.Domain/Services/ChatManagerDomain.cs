using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Domain.Validators;


namespace ChatRoomWithBot.Domain.Services
{
    public class ChatManagerDomain : IChatManagerDomain
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly EventValidator _validator;
        public ChatManagerDomain(IMediatorHandler mediatorHandler, EventValidator validator)
        {
            _mediatorHandler = mediatorHandler;
            _validator = validator;
        }

        public void Dispose()
        {

            GC.SuppressFinalize(this);
        }

        public async Task<bool> JoinChatRoomAsync(Guid roomId, Guid userId)
        {

            var joinChatRoomCommand = new JoinChatRoomEvent(roomId: roomId, userId: userId);

            var result = await _mediatorHandler.SendMessage(joinChatRoomCommand);

            return result.Success;
        }

        public async Task<CommandResponse> SendMessageAsync(Event message)
        {
            var validate = await _validator.ValidateAsync(message);

            //var command = EventCreator.CriarChatMessageEvent(message, validate.IsValid);

            //return await _mediatorHandler.PublishCommand(command);

            return CommandResponse.Ok();
            

        }
    }
}
