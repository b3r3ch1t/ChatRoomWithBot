using ChatRoomWithBot.Application.Interfaces;
using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Entities;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces.Repositories;

namespace ChatRoomWithBot.Application.Services
{
    internal class ChatManagerApplication : IChatManagerApplication
    {

         
        private readonly IChatManagerDomain _chatManagerDomain;
        private readonly IBerechitLogger _berechitLogger;
        private readonly IChatMessageRepository _chatMessageRepository;

        public ChatManagerApplication(IChatManagerDomain chatManagerDomain, IBerechitLogger berechitLogger, IChatMessageRepository chatMessageRepository)
        {
           
            _chatManagerDomain = chatManagerDomain;
            _berechitLogger = berechitLogger;
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<CommandResponse> SendMessageAsync(SendMessageViewModel model)
        {

            try
            {

                Event chatMessageEvent;

                if (model.IsCommand)
                {
                    chatMessageEvent = new ChatMessageCommandEvent()
                    {
                        CodeRoom = model.RoomId,
                        Message = model.Message,
                        UserId = Guid.NewGuid() ,
                        UserName = "bot"
                    };

                }
                else
                {
                    chatMessageEvent = new ChatMessageTextEvent
                    {
                        CodeRoom = model.RoomId,
                        Message = model.Message,
                        UserId = model.UserId.Value,
                        UserName = model.UserName
                    };
                }
               

                var result = await _chatManagerDomain.SendMessageAsync(chatMessageEvent);

                return result;

            }
            catch (Exception e)
            {
                _berechitLogger.Error(e);
                return CommandResponse.Fail(e);
            }

        }

        public async Task<CommandResponse> AddCommitedAsync(ChatMessage chatMessage)
        {
            try
            {
                if (chatMessage.Message.StartsWith("This command is not valid : /")) return CommandResponse.Ok();

                return await _chatMessageRepository.AddCommitedAsync(chatMessage);
            }
            catch (Exception e)
            {
                _berechitLogger.Error(e);

                return CommandResponse.Fail(e);
            }
        }

        public async Task<IEnumerable<ChatMessageViewModel>> GetMessagesAsync(Guid roomId, int qte)
        {
            var result = _chatMessageRepository.GetLastMessagesAsync(qte, roomId);

            

            var map = result.Select(x => new ChatMessageViewModel()
            {
                UserName = x.UserName,
                Date = x.DateCreated  ,
                Message = x.Message,
                RoomId = x.RoomId
            });

            return map;
        }








        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}
