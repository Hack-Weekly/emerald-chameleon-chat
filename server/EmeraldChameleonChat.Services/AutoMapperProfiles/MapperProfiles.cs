using AutoMapper;
using EmeraldChameleonChat.Services.Model.DTO.Users;
using EmeraldChameleonChat.Services.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmeraldChameleonChat.Services.AutoMapperProfiles
{
    public class MapperProfiles : Profile
    {

        public MapperProfiles()
        {
            UsersMapper();
        }

        private void UsersMapper()
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
        }
    }
}
