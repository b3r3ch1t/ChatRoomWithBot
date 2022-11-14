using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Entities;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Domain.Interfaces.Repositories;
using ChatRoomWithBot.UI.MVC.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoomWithBot.UI.MVC.Handles;

public class ChatRoomHandler :
    IRequestHandler<ChatMessageTextEvent, CommandResponse>
{
    private readonly IHubContext<ChatRoomHub> _hubContext;
    private readonly IBerechitLogger _berechitLogger; 
    private readonly IChatMessageRepository _chatMessageRepository;
    public ChatRoomHandler(IHubContext<ChatRoomHub> hubContext, IBerechitLogger berechitLogger,  IChatMessageRepository chatMessageRepository)
    {
        _hubContext = hubContext;
        _berechitLogger = berechitLogger;
        _chatMessageRepository = chatMessageRepository; 
    }



    public async Task<CommandResponse> Handle(ChatMessageTextEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var group = notification.CodeRoom.ToString();
            var user = notification.UserName;
            var message = notification.Message;

            await _hubContext.Clients.Group(group)
                .SendAsync("ReceiveMessage", user, message);


            var chatMessage = new ChatMessage(userId: notification.UserId, message:notification.Message, roomId: notification.CodeRoom);

            var result = await  _chatMessageRepository.AddCommitedAsync(chatMessage);
            

            return result ;
        }
        catch (Exception e)
        {
            _berechitLogger.Error(e);
            return CommandResponse.Fail(e);
        }
    }
}