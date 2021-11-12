using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WA.Pizza.Core.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<T> GetByIdAsync(
            Expression<Func<T, bool>> expression,
            Expression<Func<T, object>> include);

        IQueryable<T> FindByCondition(
             Expression<Func<T, bool>> expression,
             Expression<Func<T, object>> include);
        IQueryable<T> GetAllAsync();
        Task<T> CreateAsync(T entitie);
        Task<T> UpdateAsync(T entitie);
        Task<T> DeleteAsync(T entitie);
    }
}
