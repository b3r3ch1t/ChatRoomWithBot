using ChatRoomWithBot.Domain.Interfaces;


namespace ChatRoomWithBot.Domain.Services
{
    public  class ChatManagerDomain: IChatManagerDomain
    {
        private readonly IChatManagerSignalR _chatManagerSignalR;

        public ChatManagerDomain(IChatManagerSignalR chatManagerSignalR)
        {
            _chatManagerSignalR = chatManagerSignalR;
        }

        public void Dispose()
        {
            _chatManagerSignalR.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task<bool> JoinChatRoomAsync(Guid roomId, Guid userId)
        {
            return _chatManagerSignalR.JoinChatRoomAsync(roomId: roomId, userId: userId); 
        }
    }
}
