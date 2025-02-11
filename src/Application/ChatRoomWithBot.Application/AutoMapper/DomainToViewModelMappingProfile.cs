using AutoMapper;
using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Domain.Entities;

namespace ChatRoomWithBot.Application.AutoMapper;

internal class DomainToViewModelMappingProfile : Profile
{
    internal DomainToViewModelMappingProfile()
    {

       CreateMap<ChatRoom, ChatRoomViewModel>()
            .ForMember(dest => dest.ChatRoomId,
                o => o.MapFrom(map => map.Id))
            ;

        CreateMap<ChatMessage, ChatMessageViewModel>()
            .ForMember(dest => dest.Date ,
                o => o.MapFrom(map => map.DateCreated ))
            .ForMember(dest => dest.UserName ,
                o => o.MapFrom(map => map.UserName))
            ; 
    }

}