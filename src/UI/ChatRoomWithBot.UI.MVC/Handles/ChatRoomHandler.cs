using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Commands;
using ChatRoomWithBot.UI.MVC.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoomWithBot.UI.MVC.Handles;

public class ChatRoomHandler : IRequestHandler<JoinChatRoomCommand, CommandResponse>
{
    private readonly IHubContext<ChatRoomHub> _hubContext;

    public ChatRoomHandler(IHubContext<ChatRoomHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task<CommandResponse> Handle(JoinChatRoomCommand notification, CancellationToken cancellationToken)
    {

        try
        {

            var x = _hubContext.Clients;

            await _hubContext.Groups.AddToGroupAsync(connectionId: notification.UserId.ToString(),
                groupName: notification.RoomId.ToString(), cancellationToken: cancellationToken);


            return (CommandResponse.Ok());
        }
        catch (Exception e)
        {
            
            Console.WriteLine(e);
            return CommandResponse.Fail(e);
        }
       

    }




}