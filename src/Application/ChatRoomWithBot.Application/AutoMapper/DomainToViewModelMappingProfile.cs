﻿using AutoMapper;
using ChatRoomWithBot.Application.ViewModel;
using ChatRoomWithBot.Data.IdentityModel;

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
    }

}