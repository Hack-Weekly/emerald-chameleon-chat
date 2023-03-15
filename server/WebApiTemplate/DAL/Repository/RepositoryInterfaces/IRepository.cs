using Microsoft.AspNetCore.Routing.Template;
using EmeraldChameleonChat.Model.Entity;

namespace EmeraldChameleonChat.DAL.Repository.RepositoryInterfaces
{
    public interface IRepository<TEntity> 
        where TEntity : class, IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="token"></param>
        /// <param name="save"></param>
        /// <returns>A <see cref="Task{TResult}"/> respresenting the result of the asynchronus operation</returns>
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken token, bool save = true);
        Task DeleteAsync(Guid id, CancellationToken token, bool save = true);
        Task<IEnumerable<TEntity>> GetAsync(CancellationToken token);
        Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<int> SaveAsync(CancellationToken token);
    }
}
