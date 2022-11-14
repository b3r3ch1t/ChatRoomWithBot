﻿using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain.Bus;

namespace ChatRoomWithBot.Application.Interfaces;

public interface IChatManagerApplication : IDisposable
{
    Task<IEnumerable<ChatRoomViewModel>> GetChatRoomsAsync();
    Task<bool> JoinChatRoomAsync(Guid roomId, Guid userId);
    Task<ChatRoomViewModel> GetChatRoomByIdAsync(Guid roomId);
    Task<CommandResponse> SendMessageAsync(Guid roomId, string message, Guid userId);
}