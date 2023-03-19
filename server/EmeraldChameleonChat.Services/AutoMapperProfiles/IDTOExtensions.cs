using System.Runtime.CompilerServices;
using AutoMapper;
using EmeraldChameleonChat.Services.Model.DTO;
using EmeraldChameleonChat.Services.Model.Entity;

namespace EmeraldChameleonChat.Services.AutoMapperProfiles
{
    public static class IDTOExtensions
    {
        private static readonly IMapper AutoMapper =
            new Mapper(new MapperConfiguration(ex =>
            {
                ex.AddProfile(new EntityToDTOProfile());
                ex.AddProfile(new DTOToEntityProfile());
                ex.AddProfile(new MapperProfiles());
            }));

        public static TEntity MapToEntity<TEntity>(this IDTO dto)
            where TEntity : class, IEntity
        {
            return AutoMapper.Map<TEntity>(dto);
        }

        public static TEntity MapToEntity<TEntity>(this IEnumerable<IDTO> dto)
            where TEntity : IEnumerable<IEntity>
        {
            return AutoMapper.Map<TEntity>(dto);
        }
    }
}
