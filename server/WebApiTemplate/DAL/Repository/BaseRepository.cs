using Microsoft.EntityFrameworkCore;
using EmeraldChameleonChat.DAL.Repository.RepositoryInterfaces;
using EmeraldChameleonChat.Model.Entity;

namespace EmeraldChameleonChat.DAL.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken token, bool save = true)
        {
            GenerateIdIfRequired(entity);
            await _context.Set<TEntity>().AddAsync(entity, token);
            await _context.SaveChangesAsync(token);
            return entity;
        }

        private void GenerateIdIfRequired(TEntity entity)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken token, bool save = true)
        {
            var existing = await GetAsync(id, token);
            if (existing is not null)
            {
                _context.Remove(existing);
                await _context.SaveChangesAsync(token);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAsync(CancellationToken token)
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity?> GetAsync(Guid id, CancellationToken token)
        {
            return await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken: token);
        }

        public async Task<int> SaveAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token);
        }
    }
}
