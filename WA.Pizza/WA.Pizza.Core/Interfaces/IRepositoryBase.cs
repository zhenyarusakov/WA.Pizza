using System.Linq;
using System.Threading.Tasks;
using WA.Pizza.Core.Entities;

namespace WA.Pizza.Core.Interfaces
{
    public interface IRepositoryBase
    {
        Task<T> GetById<T>(int id)
            where T : BaseEntity;
        IQueryable<T> GetAllAsync<T>()
            where T : BaseEntity;
        Task<T> CreateAsync<T>(T entitie)
            where T : BaseEntity;
        Task<T> UpdateAsync<T>(T entitie)
            where T : BaseEntity;
        Task<T> DeleteAsync<T>(T entitie)
            where T : BaseEntity;
    }
}
