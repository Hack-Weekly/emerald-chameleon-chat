using EmeraldChameleonChat.Services.Model.DTO;
using EmeraldChameleonChat.Services.Model.Entity;
using AutoMapper;

namespace EmeraldChameleonChat.Services.AutoMapperProfiles
{
    public class DTOToEntityProfile : Profile
    {
        public DTOToEntityProfile()
        {
            CreateMap<ChatRoomMessageDTO, ChatRoomMessage>();
            //GET mappings
            CreateMap<WeatherForecastGetDto, WeatherForecast>();

            //POST mappings
        }
    }
}
