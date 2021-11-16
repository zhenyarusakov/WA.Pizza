using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
using WA.Pizza.Core.Interfaces;
using WA.Pizza.Infrastructure.Data;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public class RepositoryBase<T> : IRepository<T> where T: BaseEntity
    {
        private readonly WAPizzaContext _context;
        public RepositoryBase(WAPizzaContext context)
        {
            _context = context;
        }

        public async Task<T> GetById(int id) 
        {
            return await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
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
