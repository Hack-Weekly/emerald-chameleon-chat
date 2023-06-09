﻿using EmeraldChameleonChat.Services.Model.DTO;
using EmeraldChameleonChat.Services.Model.Entity;
using AutoMapper;

namespace EmeraldChameleonChat.Services.AutoMapperProfiles
{
    public class DTOToEntityProfile : Profile
    {
        public DTOToEntityProfile()
        {
            CreateMap<ChatRoomMessageDto, ChatRoomMessage>();

            CreateMap<ChatRoomDto, ChatRoom>();
            //GET mappings
            CreateMap<WeatherForecastGetDto, WeatherForecast>();

            //POST mappings
        }
    }
}
