namespace ChatRoomWithBot.Domain.Events
{
    internal static class EventCreator
    {
        public static Event CriarChatMessageEvent(Event message, bool isValid)
        {
            // if (!isValid)
            //  {
            //  return new ChatMessageInvalidEvent(message: message.Message, codeRoom: message.CodeRoom);

            //   }


            //if (message.IsBotCommand())
            //{
            //    return new ChatMessageBotEvent(message: message.Message, codeRoom: message.CodeRoom,
            //        userId: message.UserId); 
            //}


            //return new ChatMessageTextEvent(message: message.Message, codeRoom: message.CodeRoom,
            //    userId: message.UserId);


            return null;
        }
    }
}
