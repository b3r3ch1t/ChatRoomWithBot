using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Domain.Entities;
using ChatRoomWithBot.Domain.Interfaces;

namespace ChatRoomWithBot.Data.Repository;

public class ChatMessageRepository :Repository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(ChatRoomWithBotContext context, IError error) : base(context, error)
    {
    }

    public async Task<IQueryable<ChatMessage>> GetAllMessagesAsync(int qte)
    {
         
            var result = Context.ChatMessages
                .OrderByDescending(x => x.DateCreated)
                .Take(qte);

            return result; 
        
    }

     
}