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
        private readonly IBerechitLogger _berechitLogger;

        public ChatManagerDomain(IMediatorHandler mediatorHandler, EventValidator validator, IBerechitLogger berechitLogger)
        {
            _mediatorHandler = mediatorHandler;
            _validator = validator;
            _berechitLogger = berechitLogger;

        }

        public void Dispose()
        {

            GC.SuppressFinalize(this);
        }

       
        public async Task<CommandResponse> SendMessageAsync(Event message)
        {
            var validate = await _validator.ValidateAsync(message);
 
            CommandResponse result;

            try
            {
                if (!validate.IsValid)
                {
                    message.Message = $"This message is not valid : {message.Message}";
                    result = await _mediatorHandler.SendMessage(message);
                    return result;
                }

                if (message.IsCommand)
                {
                    message.UserName = "bot";

                    result = await _mediatorHandler.SendMessage(message);
                    return result;
                }

            }
            catch (Exception e)
            {
              _berechitLogger.Error(e);
              return CommandResponse.Fail(e);
            }

            result = await  _mediatorHandler.SendMessage(message);

            return result;

        }
         
    }
}
