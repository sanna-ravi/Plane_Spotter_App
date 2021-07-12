using Spotters.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spotters.Data.Repository
{
    public interface IBaseRepository<T>
         where T : class, IBaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> FindAsync(object keys);
        IQueryable<T> FindQueryById(Guid id);
        Task<T> FindByIdAsync(Guid id);
        Task<T> FindByConditionAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
        Task<T> CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity);
        Task UpdateListAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteListAsync(IEnumerable<T> entities);
    }
}
