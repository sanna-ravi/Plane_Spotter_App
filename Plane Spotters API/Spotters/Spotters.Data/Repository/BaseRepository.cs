using Microsoft.EntityFrameworkCore;
using Spotters.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spotters.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class, IBaseEntity
    {
        protected DbSet<T> SpottersDBContext { get; set; }

        public BaseRepository(DbSet<T> spottersDBContext)
        {
            this.SpottersDBContext = spottersDBContext;
        }

        public IQueryable<T> GetAll()
        {
            return SpottersDBContext.Where(x => !x.IsDeleted);
        }

        public async Task<T> FindAsync(object keys)
        {
            var entity = await SpottersDBContext.FindAsync(keys);
            return (entity != null && !entity.IsDeleted) ? entity : null;
        }

        public IQueryable<T> FindQueryById(Guid id)
        {
            return GetAll().Where(x => x.InternalId.Equals(id));
        }

        public async Task<T> FindByIdAsync(Guid id)
        {
            var entity = await GetAll().Where(x => x.InternalId.Equals(id)).FirstOrDefaultAsync().ConfigureAwait(true);
            return (entity != null && !entity.IsDeleted) ? entity : null;
        }

        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await GetAll().Where(expression).FirstOrDefaultAsync().ConfigureAwait(true);
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return GetAll().Where(expression);
        }

        public async Task<T> CreateAsync(T entity)
        {
            return (await this.SpottersDBContext.AddAsync(entity)).Entity;
        }

        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {
            await SpottersDBContext.AddRangeAsync(entities).ConfigureAwait(true);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return (await Task.Run(() => SpottersDBContext.Update(entity)).ConfigureAwait(true)).Entity;
        }

        public async Task UpdateListAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => SpottersDBContext.UpdateRange(entities)).ConfigureAwait(true);
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity != null)
            {
                entity.IsDeleted = true;
                await UpdateAsync(entity).ConfigureAwait(true);
            }
        }

        public async Task DeleteListAsync(IEnumerable<T> entities)
        {
            if (entities != null)
            {
                foreach (T entity in entities)
                {
                    entity.IsDeleted = true;
                }
                await UpdateListAsync(entities).ConfigureAwait(true);
            }
        }
    }
}
