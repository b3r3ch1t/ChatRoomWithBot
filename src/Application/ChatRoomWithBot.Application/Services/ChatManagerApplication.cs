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
        private readonly IBerechitLogger _berechitLogger;

        public ChatManagerApplication(IChatRoomRepository chatRoomRepository, IMapper mapper, IChatManagerDomain chatManagerDomain, IBerechitLogger berechitLogger)
        {
            _chatRoomRepository = chatRoomRepository;
            _mapper = mapper;
            _chatManagerDomain = chatManagerDomain;
            _berechitLogger = berechitLogger;
        }

        public async Task<CommandResponse> SendMessageAsync(ISendMessageViewModel model)
        {
 
            try
            {
                var chatMessageEvent = _mapper.Map<Event>(model);

                var result = await _chatManagerDomain.SendMessageAsync(chatMessageEvent);

                return result;
            }
            catch (Exception e)
            {
                _berechitLogger.Error(e);
                return CommandResponse.Fail(e);
            }
           
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
