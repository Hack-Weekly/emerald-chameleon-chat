using AutoMapper;
using System.Runtime.CompilerServices;
using EmeraldChameleonChat.Model.DTO;
using EmeraldChameleonChat.Model.Entity;

namespace EmeraldChameleonChat.AutoMapperProfiles
{
    public static class IDTOExtensions
    {
        private static readonly IMapper AutoMapper =
            new Mapper(new MapperConfiguration(ex =>
            {
                ex.AddProfile(new EntityToDTOProfile());
                ex.AddProfile(new DTOToEntityProfile());
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
