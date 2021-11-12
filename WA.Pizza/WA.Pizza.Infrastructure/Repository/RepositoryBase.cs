using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WA.Pizza.Core.Interfaces;
using WA.Pizza.Infrastructure.DbContexts;

namespace WA.Pizza.Infrastructure.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly WAPizzaContext _context;
        public RepositoryBase(WAPizzaContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(
            Expression<Func<T, bool>> expression,
            Expression<Func<T, object>> include = default)
        {
            return await FindByCondition(expression, include).FirstOrDefaultAsync();
        }


        public IQueryable<T> FindByCondition(
            Expression<Func<T, bool>> expression,
            Expression<Func<T, object>> include = default)
        {
            var query = _context.Set<T>().Where(expression);

            if (include != null)
            {
                query = query.Include(include);
            }

            return query;
        }

        public IQueryable<T> GetAllAsync()
        {
            return _context.Set<T>();
        }

        public async Task<T> CreateAsync(T entitie)
        {
            _context.Set<T>().Add(entitie);

            await _context.SaveChangesAsync();

            return entitie;
        }
        public async Task<T> UpdateAsync(T entitie)
        {
            _context.Set<T>().Update(entitie);

            await _context.SaveChangesAsync();

            return entitie;
        }

        public async Task<T> DeleteAsync(T entitie)
        {
            _context.Set<T>().Remove(entitie);

            await _context.SaveChangesAsync();

            return entitie;
        }
    }
}
