using System.Text.Json;
using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Entities;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.UI.MVC.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoomWithBot.UI.MVC.Handles;

public class ChatRoomHandler :
    IRequestHandler<ChatMessageTextEvent, CommandResponse>,
    IRequestHandler<ChatResponseCommandEvent, CommandResponse>
{
    private readonly IHubContext<ChatRoomHub> _hubContext;
    private readonly IBerechitLogger _berechitLogger;
    private readonly IChatManagerApplication _chatManagerApplication;

    const int qteMessages = 50;

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
            var chatMessage = new ChatMessage(userId: notification.UserId, message: notification.Message, userName: notification.UserName, roomId: notification.CodeRoom);
          
            var result = CommandResponse.Fail("Fail to send");

            var messages = (await _chatManagerApplication.GetMessagesAsync(notification.CodeRoom, qteMessages)).ToList();


            result = await _chatManagerApplication.AddCommitedAsync(chatMessage);


            messages.Add(new ChatMessageViewModel()
            {
                Date = DateTime.Now,
                Message = notification.Message,
                RoomId = notification.CodeRoom,
                UserName = notification.UserName ,
            });

            var message = JsonSerializer.Serialize(messages);


            await _hubContext.Clients.Group(group)
                .SendAsync("ReceiveMessage", user, message);





            return result;
        }
        catch (Exception e)
        {
            _berechitLogger.Error(e);
            return CommandResponse.Fail(e);
        }
    }

    public async Task<CommandResponse> Handle(ChatResponseCommandEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var group = notification.CodeRoom.ToString();
            var user = notification.UserName;
            var chatMessage = new ChatMessageViewModel
            {
                UserName = notification.UserName,
                Date = DateTime.Now,
                Message = notification.Message,
                RoomId = notification.CodeRoom,
            };


            var messages = (await _chatManagerApplication.GetMessagesAsync(notification.CodeRoom, qteMessages)).ToList();

            messages.Add(chatMessage);

            var message = JsonSerializer.Serialize(messages);


            await _hubContext.Clients.Group(group)
                .SendAsync("ReceiveMessage", user, message);

            return CommandResponse.Ok();
        }
        catch (Exception e)
        {
            _berechitLogger.Error(e);
            return CommandResponse.Fail(e);
        }
    }
}