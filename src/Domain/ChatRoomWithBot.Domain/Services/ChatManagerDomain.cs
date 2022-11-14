﻿using ChatRoomWithBot.Domain.Bus;
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

      

        public async Task<CommandResponse> SendMessageFromUserAsync(ChatMessageFromUserEvent message)
        {
            var validate = await _validator.ValidateAsync(message);

            ChatMessageFromUserEvent publish;
            CommandResponse result;


            if (!validate.IsValid)
            {
                publish = new ChatMessageFromUserEventInvalid()
                {
                    CodeRoom = message.CodeRoom,
                    IsBotCommand = message.IsBotCommand,
                    Message = message.Message,
                    UserId = message.UserId
                };

                result = await _mediatorHandler.SendMessage(publish);
                return result;
            }

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

            result = await _mediatorHandler.SendMessage(publish);
            return result;


            return CommandResponse.Fail("Fail");
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
