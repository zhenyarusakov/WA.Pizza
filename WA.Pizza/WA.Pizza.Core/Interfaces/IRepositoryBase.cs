using System.Linq;
using System.Threading.Tasks;
using WA.Pizza.Core.Entities;

namespace WA.Pizza.Core.Interfaces
{
    public interface IRepositoryBase<T> where T: class
    {
        Task<T> GetById(int id);
        IQueryable<T> GetAllAsync();
        Task<T> CreateAsync(T entitie);
        Task<T> UpdateAsync(T entitie);
        Task<T> DeleteAsync(T entitie);
    }
}
