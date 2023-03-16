using Microsoft.EntityFrameworkCore;
using EmeraldChameleonChat.Services.DAL.Repository.RepositoryInterfaces;
using EmeraldChameleonChat.Services.Model.Entity;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EmeraldChameleonChat.Services.DAL.Repository
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
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token, bool save = true)
        {
            var entry = _context.Attach(entity);
            entry.State = EntityState.Modified;
            if (save)
            {
                await _context.SaveChangesAsync();
            }
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
