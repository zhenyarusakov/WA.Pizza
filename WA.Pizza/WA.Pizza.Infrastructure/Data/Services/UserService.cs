using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class UserService: IUserService
    {
        private readonly WAPizzaContext _context;

        public UserService(WAPizzaContext context)
        {
            _context = context;
        }
        
        public async Task<User> GetUserAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User[]> GetUsersAsync()
        {
            return await _context.Users
                .Include(x=>x.Addresses)
                .Include(x=>x.Orders)
                .ToArrayAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUserAsync(int id, User user)
        {
            var userUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userUpdate == null)
            {
                throw new ArgumentNullException($"There is no User with this {id}");
            }

            _context.Update(userUpdate);

            await _context.SaveChangesAsync();

            return userUpdate;
        }

        public async Task DeleteUserAsync(int id)
        {
            var usersDelete = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (usersDelete == null)
            {
                throw new ArgumentNullException($"There is no User with this {id}");
            }

            _context.Remove(usersDelete);
            
            await _context.SaveChangesAsync();
        }
    }
}
