using AutoMapper;
using System.Runtime.InteropServices;
using EmeraldChameleonChat.Model.DTO;
using EmeraldChameleonChat.Model.Entity;

namespace EmeraldChameleonChat.AutoMapperProfiles
{
    public class EntityToDTOProfile : Profile
    {
        public EntityToDTOProfile()
        { 
            //GET mappings
            CreateMap<WeatherForecast, WeatherForecastGetDto>();

            //POST mappings
        }
    }
}
