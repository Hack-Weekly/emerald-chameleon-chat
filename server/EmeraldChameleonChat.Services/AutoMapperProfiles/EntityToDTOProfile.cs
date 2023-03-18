using AutoMapper;
using System.Runtime.InteropServices;
using EmeraldChameleonChat.Services.Model.DTO;
using EmeraldChameleonChat.Services.Model.Entity;

namespace EmeraldChameleonChat.Services.AutoMapperProfiles
{
    public class EntityToDTOProfile : Profile
    {
        public EntityToDTOProfile()
        {
            CreateMap<ChatRoomMessage, ChatRoomMessageDTO>();
            //GET mappings
            CreateMap<WeatherForecast, WeatherForecastGetDto>();

            //POST mappings
        }
    }
}
