using AutoMapper;
using AutoMapper.Execution;
using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain.Events;

namespace ChatRoomWithBot.Application.AutoMapper;

internal class ViewModelToDomainMappingProfile : Profile
{
    public ViewModelToDomainMappingProfile()
    {

         CreateMap<SendMessageViewModel, Event>()
            .ConstructUsing((m, context) => {
                switch (m.IsCommand )
                {
                    case  true:
                        return context.Mapper.Map<ChatMessageCommandEvent>(m);
                    case false :
                        return context.Mapper.Map<ChatMessageTextEvent>(m);
                     
                }
            });



         CreateMap<SendMessageViewModel, ChatMessageTextEvent>()
             .ForMember(dest => dest.CodeRoom,
                 o => o.MapFrom(map => map.RoomId));



        CreateMap<SendMessageViewModel, ChatMessageCommandEvent>()
            .ForMember(dest => dest.CodeRoom ,
                o => o.MapFrom(map => map.RoomId));
    }
}