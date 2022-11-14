using AutoMapper;
using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Events.FromBot;
using ChatRoomWithBot.Domain.Events.FromUser;

namespace ChatRoomWithBot.Application.AutoMapper;

internal class ViewModelToDomainMappingProfile : Profile
{
    public ViewModelToDomainMappingProfile()
    {
        
        CreateMap<ISendMessageViewModel, Event>() 
            .ConstructUsing((messageViewModel, context) =>
            {
                return messageViewModel.IsBot switch
                {
                    true => context.Mapper.Map<ChatMessageFromBotEvent>(messageViewModel),
                    false => context.Mapper.Map<ChatMessageFromUserEvent>(messageViewModel)
                };
            });


        CreateMap<ISendMessageViewModel, ChatMessageFromBotEvent>();
        CreateMap<ISendMessageViewModel, ChatMessageFromUserEvent>();


    }
}