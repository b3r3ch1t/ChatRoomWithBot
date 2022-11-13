using ChatRoomWithBot.Domain.Interfaces;
 

namespace ChatRoomWithBot.Domain.Services
{
    public  class ChatManagerDomain: IChatManagerDomain
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<bool> JoinChatRoomAsync(Guid roomId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
