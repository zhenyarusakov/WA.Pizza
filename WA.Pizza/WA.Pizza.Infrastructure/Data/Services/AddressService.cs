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

        public Task<Address> GetAddressAsync(int id)
        {
            return _context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Address[]> GetAddressesAsync()
        {
            return _context.Addresses
                .Include(x=>x.User)
                .ToArrayAsync();
        }

        public async Task<Address> CreateAddressAsync(Address address)
        {
            _context.Addresses.Add(address);

            await _context.SaveChangesAsync();

            return address;
        }

        public async Task<Address> UpdateAddressAsync(Address address)
        {
            var addressUpdate = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == address.Id);

            if (addressUpdate == null)
            {
                throw new ArgumentNullException($"There is no Address with this {address.Id}");
            }

            addressUpdate.User = address.User;
            addressUpdate.City = address.City;
            addressUpdate.Country = address.Country;
            addressUpdate.Street = address.Street;
            addressUpdate.Entrance = address.Entrance;
            addressUpdate.House = address.House;
            addressUpdate.ApartmentNumber = address.ApartmentNumber;
            addressUpdate.isPrimary = address.isPrimary;

            _context.Update(addressUpdate);

            await _context.SaveChangesAsync();

            return addressUpdate;
        }

        public async Task DeleteAddressAsync(int id)
        {

            _context.Addresses.Remove(new Address()
            {
                Id = id
            });

            await _context.SaveChangesAsync();
        }
    }
}
