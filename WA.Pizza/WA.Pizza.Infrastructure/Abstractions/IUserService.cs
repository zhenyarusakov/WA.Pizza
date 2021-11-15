using System.Threading.Tasks;
using WA.Pizza.Core.Entities;

namespace WA.Pizza.Infrastructure.Abstractions
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int id);
        Task<User[]> GetUsersAsync();
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(int id, User user);
        Task DeleteUserAsync(int id);
    }
}
