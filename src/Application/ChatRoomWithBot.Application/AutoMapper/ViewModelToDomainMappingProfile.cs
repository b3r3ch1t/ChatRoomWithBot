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

        CreateMap<SendMessageViewModel, Event>()
            .ConstructUsing((messageViewModel, context) =>
            {
                return messageViewModel.IsBot switch
                {
                    true => context.Mapper.Map<ChatMessageFromBotEvent>(messageViewModel),
                    false => context.Mapper.Map<ChatMessageFromUserEvent>(messageViewModel)
                };
            });


        CreateMap<SendMessageFromBotViewModel, ChatMessageFromBotEvent>()
            .ForMember(dest => dest.Message,
                o => o.MapFrom(map => map.Message ))
            .ForMember(dest => dest.CodeRoom,
                o => o.MapFrom(map => map.RoomId))
            ;



        CreateMap<SendMessageFromUserViewModel, ChatMessageFromUserEvent>().ForMember(dest => dest.UserId,
                o => o.MapFrom(map => map.UserId))
            .ForMember(dest => dest.Message ,
                o => o.MapFrom(map => map.Message ))
            .ForMember(dest => dest.CodeRoom ,
                o => o.MapFrom(map => map.RoomId ))
            ;


    }
}