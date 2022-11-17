using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Domain.Validators;


namespace ChatRoomWithBot.Domain.Services
{
    public class ChatManagerDomain : IChatManagerDomain
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IBerechitLogger _berechitLogger;

        public ChatManagerDomain(IMediatorHandler mediatorHandler, EventValidator validator, IBerechitLogger berechitLogger)
        {
            _mediatorHandler = mediatorHandler;
            _berechitLogger = berechitLogger;

        }

        public void Dispose()
        {

            GC.SuppressFinalize(this);
        }

       
        public async Task<CommandResponse> SendMessageAsync(Event message)
        {
            try
            {
                CommandResponse result;
                if (!message.IsValid())
                {

                    if (message.Message.StartsWith("/"))
                    {
                        message = new ChatMessageTextEvent()
                        {
                            CodeRoom = message.CodeRoom,
                            Message = $"This command is not valid : {message.Message}",
                            UserId = Guid.Empty,
                            UserName = "bot"
                        };

                        result = await _mediatorHandler.SendMessage(message);

                        return result;
                    }
                    else
                    {
                        message.Message = $"This message is not valid : {message.Message}";

                    }


                    result = await _mediatorHandler.SendMessage(message);
                    return result;
                }

                if (message.IsCommand)
                {
                    message.UserName = "bot";
                    message.UserId = Guid.Empty;


                   

                    result = await _mediatorHandler.SendMessage(message);
                    return result;
                }


                result = await _mediatorHandler.SendMessage(message);
                return result;


            }
            catch (Exception e)
            {
              _berechitLogger.Error(e);
              return CommandResponse.Fail(e);
            }
 

            return CommandResponse.Fail("Fail to send message !");

        }
         
    }
}
