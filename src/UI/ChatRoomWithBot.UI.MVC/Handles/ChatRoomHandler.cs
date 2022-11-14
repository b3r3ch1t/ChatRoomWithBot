using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Entities;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.UI.MVC.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoomWithBot.UI.MVC.Handles;

public class ChatRoomHandler :
    IRequestHandler<ChatMessageTextEvent, CommandResponse>
{
    private readonly IHubContext<ChatRoomHub> _hubContext;
    private readonly IBerechitLogger _berechitLogger; 
    private readonly IChatManagerApplication _chatManagerApplication;
    public ChatRoomHandler(IHubContext<ChatRoomHub> hubContext, IBerechitLogger berechitLogger, IChatManagerApplication chatManagerApplication)
    {
        _hubContext = hubContext;
        _berechitLogger = berechitLogger;
        _chatManagerApplication = chatManagerApplication;
    }



    public async Task<CommandResponse> Handle(ChatMessageTextEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var group = notification.CodeRoom.ToString();
            var user = notification.UserName;
            var message = notification.Message;
            var chatMessage = new ChatMessage(userId: notification.UserId, message: notification.Message, roomId: notification.CodeRoom);
            
            var result = await _chatManagerApplication.AddCommitedAsync(chatMessage);

            var messages =await  _chatManagerApplication.GetMessagesAsync();

            await _hubContext.Clients.Group(group)
                .SendAsync("ReceiveMessage", user, message);


           
            

            return result ;
        }
        catch (Exception e)
        {
            _berechitLogger.Error(e);
            return CommandResponse.Fail(e);
        }
    }
}