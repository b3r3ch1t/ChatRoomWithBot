using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain.Interfaces;
using AutoMapper;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces.Repositories;

namespace ChatRoomWithBot.Application.Services
{
    internal class ChatManagerApplication : IChatManagerApplication
    {

        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IMapper _mapper;
        private readonly IChatManagerDomain _chatManagerDomain;
        public ChatManagerApplication(IChatRoomRepository chatRoomRepository, IMapper mapper, IChatManagerDomain chatManagerDomain)
        {
            _chatRoomRepository = chatRoomRepository;
            _mapper = mapper;
            _chatManagerDomain = chatManagerDomain;
        }

        public async Task<CommandResponse> SendMessageAsync(Guid roomId, string message, Guid userId)
        {

            var chatMessageEvent = new ChatMessageEvent(userId: userId, message: message, codeRoom: roomId);

            var result = await _chatManagerDomain.SendMessageAsync(chatMessageEvent);

            return result ;
        }

        public async Task<IEnumerable<ChatRoomViewModel>> GetChatRoomsAsync()
        {
            var result = await _chatRoomRepository.GetAllAsync();

            var map = _mapper.Map<IEnumerable<ChatRoomViewModel>>(result);

            return map;
        }

        public async Task<bool> JoinChatRoomAsync(Guid roomId, Guid userId)
        {

            var result = await _chatManagerDomain.JoinChatRoomAsync(roomId: roomId, userId: userId);

            return result;

        }

        public async Task<ChatRoomViewModel> GetChatRoomByIdAsync(Guid roomId)
        {
            var result = await _chatRoomRepository.GetByIdAsync(roomId);

            var map = _mapper.Map<ChatRoomViewModel>(result);

            return map;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
