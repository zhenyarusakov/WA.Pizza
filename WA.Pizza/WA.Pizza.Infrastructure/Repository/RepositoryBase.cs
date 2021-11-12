using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetAllAsync()
        {
            return _context.Set<T>().AsNoTracking();
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
