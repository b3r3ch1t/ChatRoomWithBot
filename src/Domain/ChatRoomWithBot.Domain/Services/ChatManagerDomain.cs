using System.Diagnostics.Tracing;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Events.FromBot;
using ChatRoomWithBot.Domain.Events.FromUser;
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

        public async Task<CommandResponse> SendMessageFromUserAsync(ChatMessageFromUserEvent message)
        {
            var validate = await _validator.ValidateAsync(message);

            ChatMessageFromUserEvent publish;
            
            if (!validate.IsValid)
            {
                publish = new ChatMessageFromUserEventInvalid()
                {
                    CodeRoom = message.CodeRoom,
                    IsBotCommand = message.IsBotCommand,
                    Message = message.Message,
                    UserId = message.UserId
                };
            }
            else
            {
                if (message.IsBotCommand)
                {
                    publish = new ChatMessageFromUserEventCommand()
                    {
                        CodeRoom = message.CodeRoom,
                        IsBotCommand = message.IsBotCommand,
                        Message = message.Message,
                        UserId = message.UserId
                    };
                }
                else
                {
                    publish = new ChatMessageFromUserEventText()
                    {
                        CodeRoom = message.CodeRoom,
                        IsBotCommand = message.IsBotCommand,
                        Message = message.Message,
                        UserId = message.UserId
                    };
                }
            }

            var result = await _mediatorHandler.SendMessage(publish); 

            return result ;
        }


        public async Task<CommandResponse> SendMessageFromBotAsync(ChatMessageFromBotEvent message)
        {
            var validate = await _validator.ValidateAsync(message);

            ChatMessageFromBotEvent publish;


            if (!validate.IsValid)
            {
                publish = new ChatMessageFromBotEventInvalid()
                {
                    CodeRoom = message.CodeRoom,
                    IsBotCommand = message.IsBotCommand,
                    Message = message.Message
                };
            }
            else
            {
                if (message.IsBotCommand)
                {
                    publish = new ChatMessageFromBotEventCommand()
                    {
                        CodeRoom = message.CodeRoom,
                        IsBotCommand = message.IsBotCommand,
                        Message = message.Message
                    };
                }
                else
                {
                    publish = new ChatMessageFromBotEventText()
                    {
                        CodeRoom = message.CodeRoom,
                        IsBotCommand = message.IsBotCommand,
                        Message = message.Message
                    };
                }
            }

            var result = await _mediatorHandler.SendMessage(publish);

            return result;


        }
    }
}
