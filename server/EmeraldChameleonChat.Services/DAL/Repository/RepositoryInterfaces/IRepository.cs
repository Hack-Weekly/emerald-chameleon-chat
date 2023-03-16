using EmeraldChameleonChat.Services.Model.Entity;

namespace EmeraldChameleonChat.Services.DAL.Repository.RepositoryInterfaces
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
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token, bool save = true);
        Task DeleteAsync(Guid id, CancellationToken token, bool save = true);
        Task<IEnumerable<TEntity>> GetAsync(CancellationToken token);
        Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<int> SaveAsync(CancellationToken token);
    }
}
