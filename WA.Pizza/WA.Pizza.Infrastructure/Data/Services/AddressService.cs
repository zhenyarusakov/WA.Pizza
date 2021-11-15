using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WA.Pizza.Core.Entities.OrderDomain;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Infrastructure.Data.Services
{
    public class AddressService: IAddressService
    {
        private readonly WAPizzaContext _context;

        public AddressService(WAPizzaContext context)
        {
            _context = context;
        }

        public async Task<Address> GetAddressAsync(int id)
        {
            return await _context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Address[]> GetAddressesAsync()
        {
            return await _context.Addresses
                .Include(x=>x.User)
                .ToArrayAsync();
        }

        public async Task<Address> CreateAddressAsync(Address address)
        {
            _context.Addresses.Add(address);

            await _context.SaveChangesAsync();

            return address;
        }

        public async Task<Address> UpdateAddressAsync(int id)
        {
            var addressUpdate = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == id);

            if (addressUpdate == null)
            {
                throw new ArgumentNullException($"There is no Address with this {id}");
            }

            _context.Update(addressUpdate);

            await _context.SaveChangesAsync();

            return addressUpdate;
        }

        public async Task DeleteAddressAsync(int id)
        {
            var addressDelete = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == id);

            if (addressDelete == null)
            {
                throw new ArgumentNullException($"There is no Address with this {id}");
            }

            _context.Remove(addressDelete);

            await _context.SaveChangesAsync();
        }
    }
}
