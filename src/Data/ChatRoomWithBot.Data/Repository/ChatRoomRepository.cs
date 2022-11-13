using ChatRoomWithBot.Domain.Entities;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Domain.Interfaces.Repositories;

namespace ChatRoomWithBot.Data.Repository
{
    public  class ChatRoomRepository:Repository<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(ChatRoomWithBotContext context, IError error) : base(context, error)
        {
        }
    }
}
