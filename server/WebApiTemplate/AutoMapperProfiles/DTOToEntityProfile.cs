using EmeraldChameleonChat.Model.DTO;
using EmeraldChameleonChat.Model.Entity;
using AutoMapper;

namespace EmeraldChameleonChat.AutoMapperProfiles
{
    public class DTOToEntityProfile : Profile
    {
        public DTOToEntityProfile() 
        {
            //GET mappings
            CreateMap<WeatherForecastGetDto, WeatherForecast>();

            //POST mappings
        }
    }
}
