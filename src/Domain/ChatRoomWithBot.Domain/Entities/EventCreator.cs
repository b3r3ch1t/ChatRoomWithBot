﻿using ChatRoomWithBot.Domain.Events;

namespace ChatRoomWithBot.Domain.Entities
{
    internal static  class EventCreator
    {
        public static Event CriarChatMessageEvent(ChatMessageEvent message, bool isValid)
        {
            if (!isValid)
            {
                return new ChatMessageInvalidEvent(message: message.Message, codeRoom: message.CodeRoom);
                 
            }


            if (message.IsBotCommand())
            {
                return new ChatMessageBotEvent(message: message.Message, codeRoom: message.CodeRoom,
                    userId: message.UserId); 
            }


            return new ChatMessageTextEvent(message: message.Message, codeRoom: message.CodeRoom,
                userId: message.UserId);

        }
    }
}
