using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events.FromUser;
using ChatRoomWithBot.UI.MVC.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoomWithBot.UI.MVC.Handles;

public class ChatRoomHandler :
    IRequestHandler<ChatMessageFromUserEventInvalid, CommandResponse>,
    IRequestHandler<ChatMessageFromUserEventText, CommandResponse>
{
    private readonly IHubContext<ChatRoomHub> _hubContext;

    public ChatRoomHandler(IHubContext<ChatRoomHub> hubContext)
    {
        _hubContext = hubContext;
    }


    public async Task<CommandResponse> Handle(ChatMessageFromUserEventInvalid notification, CancellationToken cancellationToken)
    {
        try
        {
            var group = notification.CodeRoom.ToString();
            var user = notification.UserName;
            var message = notification.Message;

            await _hubContext.Clients.Group(group)
                .SendAsync("ReceiveMessage", user, message);

            return CommandResponse.Ok();
        }
        catch (Exception e)
        {
            //_berechitLogger.Error(e);
            return CommandResponse.Fail(e);
        }
    }


    public async Task<CommandResponse> Handle(ChatMessageFromUserEventText notification, CancellationToken cancellationToken)
    {
        try
        {
            var group = notification.CodeRoom.ToString();
            var user = notification.UserId.ToString();
            var message = notification.Message ;

            await _hubContext.Clients.Group(group)
                .SendAsync("ReceiveMessage", user, message);

            return CommandResponse.Ok();
        }
        catch (Exception e)
        {
            //_berechitLogger.Error(e);
            return CommandResponse.Fail(e);
        }
    }
}