using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WA.Pizza.Core.Entities;
using WA.Pizza.Core.Interfaces;
using WA.Pizza.Infrastructure.DbContexts;

namespace WA.Pizza.Infrastructure.Repository
{
    public class RepositoryBase : IRepositoryBase
    {
        private readonly WAPizzaContext _context;
        public RepositoryBase(WAPizzaContext context)
        {
            _context = context;
        }

        public async Task<T> GetById<T>(int id) 
            where T : BaseEntity
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetAllAsync<T>()
             where T : BaseEntity
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> CreateAsync<T>(T entitie)
             where T : BaseEntity
        {
            _context.Set<T>().Add(entitie);

            await _context.SaveChangesAsync();

            return entitie;
        }
        public async Task<T> UpdateAsync<T>(T entitie)
             where T : BaseEntity
        {
            _context.Set<T>().Update(entitie);

            await _context.SaveChangesAsync();

            return entitie;
        }

        public async Task<T> DeleteAsync<T>(T entitie)
             where T : BaseEntity
        {
            _context.Set<T>().Remove(entitie);

            await _context.SaveChangesAsync();

            return entitie;
        }
    }
}
