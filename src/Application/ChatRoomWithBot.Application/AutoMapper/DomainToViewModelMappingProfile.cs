using AutoMapper;
using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Data.IdentityModel;
using ChatRoomWithBot.Domain.Entities;

namespace ChatRoomWithBot.Application.AutoMapper;

internal class DomainToViewModelMappingProfile : Profile
{
    internal DomainToViewModelMappingProfile()
    {

        CreateMap<UserIdentity, UserViewModel>()
            .ForMember(dest => dest. Id ,
                o => o.MapFrom(map => map.Id ))
            .ForMember(dest => dest.Name ,
                o => o.MapFrom(map => map.Name ))

            .ForMember(dest => dest.Email,
                o => o.MapFrom(map => map.Email))

            ;


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