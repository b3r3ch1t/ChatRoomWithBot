using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Entities;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Domain.Interfaces.Repositories;

namespace ChatRoomWithBot.Data.Repository;

public class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
{
    private readonly IBerechitLogger _berechitLogger;

    public ChatMessageRepository(ChatRoomWithBotContext context, IBerechitLogger berechitLogger) : base(context,
        berechitLogger)
    {
        _berechitLogger = berechitLogger;
    }

    public async Task<IQueryable<ChatMessage>> GetAllMessagesAsync(int qte)
    {

        var result = Context.ChatMessages
            .OrderByDescending(x => x.DateCreated)
            .Take(qte);

        return result;

    }

    public async Task<CommandResponse> AddCommitedAsync(ChatMessage chatMessage)
    {
        try
        {
            await Context.ChatMessages.AddAsync(chatMessage);

            await Context.SaveChangesAsync();

            return CommandResponse.Ok();

        }
        catch (Exception e)
        {
            _berechitLogger.Error(e);
            return CommandResponse.Fail("Erro salving ChatMessage");
        }
    }

    public IEnumerable<ChatMessage> GetLastMessagesAsync(int qte, Guid roomId)
    {

        try
        {
            return Context.ChatMessages
                .Where(x => x.RoomId == roomId)
                .OrderByDescending( x => x.DateCreated)
                .Take(qte).AsEnumerable();
        }
        catch (Exception e)
        {

            _berechitLogger.Error(e);
            return Enumerable.Empty<ChatMessage>();
        }
    }



}