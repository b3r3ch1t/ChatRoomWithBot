using ChatRoomWithBot.Domain.Commands;
using ChatRoomWithBot.Domain.Interfaces;


namespace ChatRoomWithBot.Domain.Services
{
    public  class ChatManagerDomain: IChatManagerDomain
    { 
        private readonly IMediatorHandler _mediatorHandler; 
        public ChatManagerDomain(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public void Dispose()
        {
            
            GC.SuppressFinalize(this);
        }

        public async  Task<bool> JoinChatRoomAsync(Guid roomId, Guid userId)
        {

            var joinChatRoomCommand = new JoinChatRoomCommand(roomId: roomId, userId: userId);

            var result =await  _mediatorHandler.SendMessage(joinChatRoomCommand); 
            
            return result.Success; 
        }
    }
}
