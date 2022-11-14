﻿using ChatRoomWithBot.Domain.Entities;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChatRoomWithBot.Data.Repository
{
    public  class ChatRoomRepository:Repository<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(ChatRoomWithBotContext context, IBerechitLogger berechitLogger) : base(context, berechitLogger)
        {
        }

        public async  Task<bool> ExistsRoomIdAsync(Guid roomId)
        {
            return await  Context.ChatRooms.AnyAsync(x => x.Id == roomId); 
        }
    }
}
