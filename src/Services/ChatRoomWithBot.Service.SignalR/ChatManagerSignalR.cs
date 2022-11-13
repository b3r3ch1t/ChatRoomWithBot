using ChatRoomWithBot.Domain.Interfaces;

namespace ChatRoomWithBot.Service.SignalR
{
    public class ChatManagerSignalR :  IChatManagerSignalR
    {
        private readonly IError _error;
        public ChatManagerSignalR(IError error)
        {
            _error = error; 
        }

        public async Task<bool> JoinChatRoomAsync(Guid roomId, Guid userId)
        {
            try
            {
              //  await Groups.Add( connectionId: userId.ToString(), groupName: roomId.ToString());
                return true;
            }
            catch (Exception e)
            {
                _error.Error(e);
                return false;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
